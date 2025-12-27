using UnityEngine;

public class EnemyVisionCS : MonoBehaviour
{
    protected EnemyChaseCS enemyChase;

    void Start()
    {
        enemyChase = GetComponentInParent<EnemyChaseCS>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyChase.SetPlayerDetected(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyChase.SetPlayerDetected(false);
        }
    }
}
