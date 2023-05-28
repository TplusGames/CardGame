using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;
    [SerializeField] private List<Transform> retaliators = new List<Transform>();

    [SerializeField] private TextMeshProUGUI damageOverlay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        damageOverlay.gameObject.SetActive(false);
    }

    public IEnumerator DealDamage(Transform Slot)
    {
        yield return new WaitForSeconds(3);

        List<Transform> neighbors = new List<Transform>();

        retaliators.Clear();

        CardSlot slotLogic = Slot.GetComponent<CardSlot>();

        if(slotLogic.upNeighbor != null)
        {
            neighbors.Add(slotLogic.upNeighbor);
        }
        if (slotLogic.downNeighbor != null)
        {
            neighbors.Add(slotLogic.downNeighbor);
        }
        if (slotLogic.leftNeighbor != null)
        {
            neighbors.Add(slotLogic.leftNeighbor);
        }
        if (slotLogic.rightNeighbor != null)
        {
            neighbors.Add(slotLogic.rightNeighbor);
        }

        foreach(Transform neighbor in neighbors)
        {
            if (slotLogic.heldByPlayer)
            {
                if (neighbor.GetComponent<CardSlot>().GetDamage() > 0)
                {
                    if (!neighbor.GetComponent<CardSlot>().heldByPlayer && neighbor.childCount > 0)
                    {
                        damageOverlay.gameObject.SetActive(true);
                        damageOverlay.text = "-" + (slotLogic.GetDamage()).ToString();
                        damageOverlay.transform.position = neighbor.transform.position;

                        if (slotLogic.GetDamage() >= neighbor.GetComponent<CardSlot>().GetDamage())
                        {
                            Destroy(neighbor.GetChild(0).gameObject);
                            neighbor.GetComponent<CardSlot>().SetDamage(0);
                            yield return new WaitForSeconds(1);
                            damageOverlay.gameObject.SetActive(false);
                        }
                        else if (slotLogic.GetDamage() < neighbor.GetComponent<CardSlot>().GetDamage())
                        {
                            neighbor.GetChild(0).GetComponent<Card>().SetDamage(neighbor.GetComponent<CardSlot>().GetDamage() - slotLogic.GetDamage());
                            neighbor.GetComponent<CardSlot>().SetDamage(neighbor.GetComponent<CardSlot>().GetDamage() - slotLogic.GetDamage());
                            retaliators.Add(neighbor);
                            yield return new WaitForSeconds(1);
                            damageOverlay.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                if (neighbor.GetComponent<CardSlot>().GetDamage() > 0)
                {
                    if (neighbor.GetComponent<CardSlot>().heldByPlayer)
                    {
                        damageOverlay.gameObject.SetActive(true);
                        damageOverlay.text = "-" + (slotLogic.GetDamage()).ToString();
                        damageOverlay.transform.position = neighbor.transform.position;

                        if (slotLogic.GetDamage() >= neighbor.GetComponent<CardSlot>().GetDamage())
                        {
                            Destroy(neighbor.GetChild(0).gameObject);
                            neighbor.GetComponent<CardSlot>().heldByPlayer = false;
                            neighbor.GetComponent<CardSlot>().SetDamage(0);
                            yield return new WaitForSeconds(1);
                            damageOverlay.gameObject.SetActive(false);
                        }
                        else if (slotLogic.GetDamage() < neighbor.GetComponent<CardSlot>().GetDamage())
                        {
                            neighbor.GetChild(0).GetComponent<Card>().SetDamage(neighbor.GetComponent<CardSlot>().GetDamage() - slotLogic.GetDamage());
                            neighbor.GetComponent<CardSlot>().SetDamage(neighbor.GetComponent<CardSlot>().GetDamage() - slotLogic.GetDamage());

                            Debug.Log(neighbor.GetComponent<CardSlot>().GetDamage());

                            retaliators.Add(neighbor);
                            yield return new WaitForSeconds(1);
                            damageOverlay.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(2);

        Debug.Log(retaliators.Count + " retaliators");

        foreach(Transform retaliator in retaliators)
        {
            damageOverlay.gameObject.SetActive(true);
            damageOverlay.text = "-" + retaliator.GetComponent<CardSlot>().GetDamage().ToString();
            damageOverlay.transform.position = Slot.position;

            slotLogic.transform.GetChild(0).GetComponent<Card>().SetDamage(slotLogic.GetDamage() - retaliator.GetComponent<CardSlot>().GetDamage());
            slotLogic.SetDamage(slotLogic.GetDamage() - retaliator.GetComponent<CardSlot>().GetDamage());

            if (slotLogic.GetDamage() <= 0)
            {
                Destroy(slotLogic.transform.GetChild(0).gameObject);
                slotLogic.SetDamage(0);
                slotLogic.heldByPlayer = false;
            }

            yield return new WaitForSeconds(1);
            damageOverlay.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        TurnManager.Instance().ChangeTurns();
    }
}
