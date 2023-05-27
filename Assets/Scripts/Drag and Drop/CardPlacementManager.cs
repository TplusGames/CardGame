using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacementManager : MonoBehaviour
{
    public static CardPlacementManager Instance;

    [SerializeField] private bool returnCard = true;

    private Transform cardSlot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one card placement manager found!");
            Destroy(this.gameObject);
        }
    }

    public void SetCardSlot(Transform slot)
    {
        cardSlot = slot;
    }

    public void SetReturnCard(bool isOnBoard)
    {
        returnCard = isOnBoard;
    }

    public bool ReturnCard()
    {
        return returnCard;
    }

    public Transform CardSlot()
    {
        return cardSlot;
    }

    public bool OnCorrectYellowSlot(CardPlacement card, Transform yellowSlot)
    {
        if (card.IsPlayerCard())
        {
            if (yellowSlot.tag != "EnemyYellow")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (yellowSlot.tag != "PlayerYellow")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
