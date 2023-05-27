using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void DealDamage(Transform Slot)
    {
        List<Transform> neighbors = new List<Transform>();

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

        foreach(Transform t in neighbors)
        {
            if (slotLogic.heldByPlayer)
            {
                if (t.GetComponent<CardSlot>().GetDamage() > 0)
                {
                    if (!t.GetComponent<CardSlot>().heldByPlayer)
                    {
                        if (slotLogic.GetDamage() > t.GetComponent<CardSlot>().GetDamage())
                        {
                            Destroy(t.GetChild(0).gameObject);
                            t.GetComponent<CardSlot>().SetDamage(0);
                        }
                    }
                }
            }
            else
            {
                if (t.GetComponent<CardSlot>().GetDamage() > 0)
                {
                    if (t.GetComponent<CardSlot>().heldByPlayer)
                    {
                        if (slotLogic.GetDamage() > t.GetComponent<CardSlot>().GetDamage())
                        {
                            Destroy(t.GetChild(0).gameObject);
                            t.GetComponent<CardSlot>().SetDamage(0);
                        }
                        else
                        {
                            Destroy(Slot.GetChild(0).gameObject);
                            slotLogic.SetDamage(0);
                        }
                    }
                }
            }
        }
    }
}
