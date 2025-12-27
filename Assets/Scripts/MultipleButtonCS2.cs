using UnityEngine;

public class MultipleButtonCS2 : MonoBehaviour
{
    public bool buttonPressedDoor2 = false;
    public DoorOpenCS doorOpenCS;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Clone"))
        {
            buttonPressedDoor2 = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Clone"))
        {
            buttonPressedDoor2 = false;
        }
    }
}
