using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private static EnemyLogic instance;

    [SerializeField] private Transform enemyHand;

    [SerializeField] private int enemyTurnTime;

    [SerializeField] private List<Transform> cards = new List<Transform>();

    [SerializeField] private SlotArray slotArray;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Build list of cards in hand
        for (int i = enemyHand.childCount - 1; i >= 0; i--)
        {
            Transform card = enemyHand.GetChild(i);
            cards.Add(card);
        }
    }

    public static EnemyLogic Instance()
    {
        return instance;
    }

    public void StartEnemyTurn()
    {
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        //Select random card from hand
        Transform cardToPlace = cards[Random.Range(0, cards.Count - 1)];

        yield return new WaitForSeconds(enemyTurnTime);

        for (int i = Random.Range(0, slotArray.Slots().Length - 1); i >= 0; i = Random.Range(0, slotArray.Slots().Length - 1))
        {
            Transform slot = slotArray.Slots()[i];
            if (slot.childCount == 0 && slot.tag != "PlayerYellow")
            {
                if (slot.GetComponent<CardSlot>().upNeighbor != null)
                {
                    if (!slot.GetComponent<CardSlot>().upNeighbor.GetComponent<CardSlot>().heldByPlayer)
                    {
                        cardToPlace.SetParent(slot);
                        cardToPlace.position = slot.position;
                        TurnManager.Instance().rowAndColumnManager.CalculateCardScore(slot, cardToPlace.GetComponent<CardPlacement>());
                        DamageManager.instance.DealDamage(slot);
                        cards.Remove(cardToPlace);
                        break;
                    }
                }
                else
                {
                    cardToPlace.SetParent(slot);
                    cardToPlace.position = slot.position;
                    TurnManager.Instance().rowAndColumnManager.CalculateCardScore(slot, cardToPlace.GetComponent<CardPlacement>());
                    cards.Remove(cardToPlace);
                    break;
                }
            }
        }
        TurnManager.Instance().ChangeTurns();
    }
}
