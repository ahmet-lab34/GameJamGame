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
    [SerializeField] private GroundCHK GroundCheck;
    private Rigidbody2D rb;
    private UIScript uIScript;

    public Animator animator;
    private PlayerInput input;
    private InputAction jumpAction;
    public bool doubleJump = false;
    [HideInInspector] public float Horizontal;

    public bool IsFacingRight;

    [SerializeField] private PlayerUIScript PlayerUIScript;

    public static UnityEngine.Vector2 checkpointPosition;
    public static bool hasCheckpoint = false;

    public AudioClip JumpingSound;
    public AudioClip[] sounds;
    private AudioSource audioSource;
    private Coroutine soundCoroutine;
    public float minInterval = 0.1f;
    public float maxInterval = 0.15f;


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
        uIScript = FindFirstObjectByType<UIScript>();

        jumpAction = input.actions.FindAction("Jump");

        audioSource = GetComponent<AudioSource>();

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

        if (Horizontal != 0 && soundCoroutine == null && GroundCheck.Grounded)
        {
            soundCoroutine = StartCoroutine(PlayRandomSounds());
        }
        else if (Horizontal == 0 && soundCoroutine != null)
        {
            StopCoroutine(soundCoroutine);
            soundCoroutine = null;
            audioSource.Stop();
        }

        animator.SetBool("Running", Horizontal > 0 || Horizontal < 0);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        if (GroundCheck.Grounded && rb.linearVelocity.y <= 0)
        {
            animator.SetBool("Jumping", false);
            Debug.Log("Character is Grounded");
        }

        if (jumpAction.triggered)
        {
            if (GroundCheck.Grounded && !doubleJump)
            {
                animator.SetBool("Jumping", true);
                Jump();
            }
            else if (doubleJump)
            {
                animator.SetBool("Jumping", true);
                Jump();
                doubleJump = !doubleJump;
            }
        }

        if ((IsFacingRight && Horizontal < 0f) || (!IsFacingRight && Horizontal > 0f))
        {
            Flip();
        }
        
        uIScript.ESC_Button();
    }

    void Jump()
    {
        audioSource.PlayOneShot(JumpingSound);
        doubleJump = true;
        animator.SetBool("Jumping", true);
        rb.AddForce(UnityEngine.Vector2.up * movingStats.jumpHeight, ForceMode2D.Impulse);
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            int randomIndex = UnityEngine.Random.Range(0, sounds.Length);
            audioSource.PlayOneShot(sounds[randomIndex]);

            yield return new WaitForSeconds(UnityEngine.Random.Range(minInterval, maxInterval));
        }
    }

    public void GetHit()
    {
        if (playerNumbers.playerSouls >= 0)
        {
            playerNumbers.playerSouls -= 1;
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
            uIScript.repeatLevel();
        }
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        UnityEngine.Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
}
