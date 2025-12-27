using UnityEngine;

public class CheckPointCS : MonoBehaviour
{
    void Start()
    {
        PlayerScript.checkpointPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript.checkpointPosition = transform.position;
            PlayerScript.hasCheckpoint = true;
        }
    }
}
