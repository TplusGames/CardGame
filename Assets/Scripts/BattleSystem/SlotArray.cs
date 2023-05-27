using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotArray : MonoBehaviour
{
    [SerializeField] private Transform[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<Transform>();
    }

    public Transform[] Slots()
    {
        return slots;
    }
}
