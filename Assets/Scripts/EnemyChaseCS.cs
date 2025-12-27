using UnityEngine;

public class EnemyChaseCS : MonoBehaviour
{
    public float speed = 2f;
    private PlayerScript playerScript;

    private Transform player;
    private bool playerDetected = false;
    private bool canAttack = true;
    private bool playerInAttackRange;
    private bool facingRight = true;
    private float attackCoolDown = 2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = FindAnyObjectByType<PlayerScript>();
    }

    void Update()
    {
        if (playerDetected)
        {
            ChasePlayer();
        }
        if (playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;

        if (direction > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction < 0 && facingRight)
        {
            Flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    void AttackPlayer()
    {
        FacePlayer();

        if (canAttack)
        {
            canAttack = false;
            Debug.Log("Enemy attacks!");

            playerScript.GetHit();

            Invoke(nameof(ResetAttack), attackCoolDown);
        }
    }
    void ResetAttack()
    {
        canAttack = true;
    }

    void FacePlayer()
    {
        float dir = player.position.x - transform.position.x;

        if (dir > 0 && !facingRight)
            Flip();
        else if (dir < 0 && facingRight)
            Flip();
    }

    
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
