using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlacement : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 returnSpot;
    private Transform returnParent;

    private Image cardUI;

    [SerializeField] Transform canvas;

    private bool isStatic = true;

    [SerializeField] private bool isPlayerCard;

    [SerializeField] private bool placed;

    public bool hasBeenCounted;

    private void OnEnable()
    {
        cardUI = GetComponent<Image>();
        if (transform.parent.gameObject.tag == "PlayerHand")
        {
            isPlayerCard = true;
            cardUI.color = Color.gray;
        }
        else
        {
            cardUI.color = Color.black;
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (!placed)
        {
            if (isPlayerCard && !TurnManager.Instance().EnemyTurn() || !isPlayerCard && TurnManager.Instance().EnemyTurn())
            {
                if (isStatic)
                {
                    //Set return position if card is not placed on a working slot
                    returnParent = transform.parent;
                    returnSpot = transform.position;
                    isStatic = false;
                }
                //Detach card from parent to properly scale mouse movement 
                transform.SetParent(canvas, true);

                //Move with mouse
                transform.position = Input.mousePosition;
                //Shut of raycasting so mouse position can detect the object behind it
                cardUI.raycastTarget = false;
            }
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (!placed)
        {
            if (isPlayerCard && !TurnManager.Instance().EnemyTurn())
            {
                if (CardPlacementManager.Instance.CardSlot().GetComponent<CardSlot>().downNeighbor != null)
                {
                    if (!CardPlacementManager.Instance.CardSlot().GetComponent<CardSlot>().downNeighbor.GetComponent<CardSlot>().heldByPlayer && CardPlacementManager.Instance.CardSlot().GetComponent<CardSlot>().downNeighbor.childCount > 0)
                    {
                        //Send card back to start position
                        transform.SetParent(returnParent, true);
                        transform.position = returnSpot;
                        return;
                    }
                }
                //Check if card is placed in a working slot
                if (!CardPlacementManager.Instance.ReturnCard() && CardPlacementManager.Instance.OnCorrectYellowSlot(this, CardPlacementManager.Instance.CardSlot().transform))
                {
                    //Set card position to center of slot and set as child for future reference
                    transform.SetParent(CardPlacementManager.Instance.CardSlot());
                    transform.position = transform.parent.position;
                    returnSpot = transform.position;
                    returnParent = transform.parent;
                    placed = true;
                    TurnManager.Instance().rowAndColumnManager.CalculateCardScore(CardPlacementManager.Instance.CardSlot(), this);
                    CardPlacementManager.Instance.CardSlot().GetComponent<CardSlot>().heldByPlayer = true;
                    StartCoroutine(DamageManager.instance.DealDamage(CardPlacementManager.Instance.CardSlot()));
                }
                else
                {
                    //Send card back to start position
                    transform.SetParent(returnParent, true);
                    transform.position = returnSpot;
                }
                isStatic = true;
                //Reengage raycasting
                cardUI.raycastTarget = true;
            }
        }
        
    }

    public bool IsPlayerCard()
    {
        return isPlayerCard;
    }

    public bool IsPlaced()
    {
        return placed;
    }
}
