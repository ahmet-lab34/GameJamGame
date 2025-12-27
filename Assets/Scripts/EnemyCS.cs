using UnityEngine;

public class EnemyCS : MonoBehaviour
{
    public GameObject Soul;
    public int enemyHealth = 5;

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(Soul, transform.position, transform.rotation);
        }
    }
}
