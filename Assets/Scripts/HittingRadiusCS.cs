using System.Collections;
using UnityEngine;

public class HittingRadiusCS : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private EnemyCS currentEnemy;
    private bool canAttack = true;
    public float attackCooldown = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        animator = GetComponentInParent<Animator>();
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
        else if (Input.GetKeyDown(KeyCode.F) && canAttack)
        {
            StartCoroutine(AttackCooldown());
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        animator.SetBool("Hitting", true);
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("Hitting", false);
        canAttack = true;
    }
}
