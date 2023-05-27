using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform upNeighbor;
    public Transform downNeighbor;
    public Transform leftNeighbor;
    public Transform rightNeighbor;

    [SerializeField] private int damage;

    public bool heldByPlayer;
    public bool hasBeenCounted;

    public void OnPointerEnter(PointerEventData data)
    {
        CardPlacementManager.Instance.SetReturnCard(false);
        CardPlacementManager.Instance.SetCardSlot(this.transform);
    }

    public void OnPointerExit(PointerEventData data)
    {
        CardPlacementManager.Instance.SetReturnCard(true);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage()
    {
        return damage;
    }
}
