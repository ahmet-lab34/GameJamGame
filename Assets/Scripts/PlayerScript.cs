using System.Numerics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{
    private GroundCHK GroundCheck;
    private Rigidbody2D rb;

    public Animator animator;
    private PlayerInput input;
    private InputAction jumpAction;
    public bool doubleJump = false;
    [HideInInspector] public float Horizontal;

    public bool IsFacingRight;

    [SerializeField] private PlayerUIScript PlayerUIScript;

    public static UnityEngine.Vector2 checkpointPosition;
    public static bool hasCheckpoint = false;

    public PlayerStats movingStats = new PlayerStats();
    private PlayerStats originalStats = new PlayerStats();

    public Numerics playerNumbers = new Numerics();

    [Serializable]
    public class PlayerStats
    {
        public float activeMoveSpeed = 5f;
        public float jumpHeight = 10f;
    }

    [Serializable]
    public class Numerics
    {
        public int playerSouls = 3;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();

        animator = GetComponentInChildren<Animator>();

        GroundCheck = FindFirstObjectByType<GroundCHK>();
        PlayerUIScript = FindFirstObjectByType<PlayerUIScript>();

        jumpAction = input.actions.FindAction("Jump");

        Time.timeScale = 1f;
    }

    void Start()
    {
        originalStats.activeMoveSpeed = movingStats.activeMoveSpeed;
        originalStats.jumpHeight = movingStats.jumpHeight;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new UnityEngine.Vector2(Horizontal * movingStats.activeMoveSpeed, rb.linearVelocity.y);
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetBool("IsRunning", Horizontal > 0 || Horizontal < 0);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        if (GroundCheck.Grounded)
        {
            animator.SetBool("IsJumping", false);
            Debug.Log("Character is Grounded");
        }

        if (jumpAction.triggered)
        {
            if (GroundCheck.Grounded && !doubleJump)
            {
                Jump();
            }
            else if (doubleJump)
            {
                Jump();
                doubleJump = !doubleJump;
            }
        }
        if (!GroundCheck.Grounded)
        {
            animator.SetBool("IsJumping", true);
        }

        if ((IsFacingRight && Horizontal < 0f) || (!IsFacingRight && Horizontal > 0f))
        {
            Flip();
        }

    }

    void Jump()
    {
        doubleJump = true;
        rb.AddForce(UnityEngine.Vector2.up * movingStats.jumpHeight, ForceMode2D.Impulse);
    }

    public void GetHit()
    {
        if (playerNumbers.playerSouls != 0)
        {
            RespawnPlayer();
        }
        else
        {
            PlayerUIScript.Die();
        }
    }

    void RespawnPlayer()
    {
        Debug.Log("Respawned the player Without CHECK");
        if (hasCheckpoint)
        {
            Debug.Log("Respawned the player");
            rb.linearVelocity = UnityEngine.Vector2.zero;
            transform.position = checkpointPosition;
        }
        else
        {
            repeatLevel();
        }
    }
    void repeatLevel()
    {
        
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        UnityEngine.Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
