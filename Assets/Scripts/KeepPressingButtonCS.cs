using System.Collections;
using UnityEngine;

public class KeepPressingButtonCS : MonoBehaviour
{
    public DoorOpenCS doorOpenCS;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Clone"))
        {
            doorOpenCS.isOpen = true;
            doorOpenCS.closeDoorr = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Clone"))
        {
            doorOpenCS.closeDoorr = true;
            doorOpenCS.isOpen = false;
        }
    }
}
