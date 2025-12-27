using System.Collections;
using UnityEngine;

public class HittingRadiusCS : MonoBehaviour
{
    private EnemyCS currentEnemy;
    private bool canAttack = true;
    public float attackCooldown = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentEnemy = other.GetComponent<EnemyCS>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && currentEnemy == other.GetComponent<EnemyCS>())
        {
            currentEnemy = null;
        }
    }

    void Update()
    {
        if (currentEnemy != null && Input.GetKeyDown(KeyCode.F) && canAttack)
        {
            currentEnemy.enemyHealth -= 1;
            Debug.Log("Enemy is damaged");
            StartCoroutine(AttackCooldown());
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
