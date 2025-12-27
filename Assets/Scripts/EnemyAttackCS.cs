using UnityEngine;

public class EnemyAttackCS : MonoBehaviour
{
    private EnemyChaseCS enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyChaseCS>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SetPlayerInAttackRange(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SetPlayerInAttackRange(false);
        }
    }
}
