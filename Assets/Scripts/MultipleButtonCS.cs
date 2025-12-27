using UnityEngine;

public class MultipleButtonCS : MonoBehaviour
{
    public bool buttonPressed = false;
    public MultipleButtonCS2 Button2;
    public DoorOpenCS doorOpenCS;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Clone"))
        {
            buttonPressed = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Clone"))
        {
            buttonPressed = false;
        }
    }
    void Update()
    {
        if (buttonPressed && Button2.buttonPressedDoor2)
        {
            doorOpenCS.isOpen = true;
        }
    }
}
