using System;
using UnityEngine;

public class ButtonCS : MonoBehaviour
{
    public DoorOpenCS doorOpenCS;
    void Start()
    {
        doorOpenCS = GetComponentInParent<DoorOpenCS>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        doorOpenCS.isOpen = true;
    }
}
