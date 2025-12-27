using UnityEngine;

public class HittingRadiusCS : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }
}
