using UnityEngine;

public class EnemyChaseCS : MonoBehaviour
{
    public float speed = 2f;
    public float attackCooldown = 2f;

    private Transform player;
    private PlayerScript playerScript;

    private bool playerDetected = false;
    private bool playerInAttackRange = false;
    private bool canAttack = true;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerScript = playerObj.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogError("Player not found! Make sure Player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player == null) return;

        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        else if (playerDetected)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    void AttackPlayer()
    {
        if (!canAttack) return;

        canAttack = false;
        Debug.Log("Enemy attacks!");

        if (playerScript != null)
        {
            playerScript.GetHit();
        }
        else
        {
            Debug.LogError("PlayerScript is null!");
        }

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    public void SetPlayerDetected(bool detected)
    {
        playerDetected = detected;
    }

    public void SetPlayerInAttackRange(bool value)
    {
        playerInAttackRange = value;
    }
}
