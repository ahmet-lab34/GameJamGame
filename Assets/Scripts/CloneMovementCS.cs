using System.Collections.Generic;
using UnityEngine;

public class CloneMovementCS : MonoBehaviour
{
    [Header("Recorded Data")]
    public List<Vector2> recordedPositions = new List<Vector2>();
    public float playbackSpeed = 1f;

    [Header("Animation Settings")]
    public float runThreshold = 0.05f;
    public float jumpThreshold = 0.02f;

    private Animator animator;
    private int currentIndex = 0;
    private Vector2 lastPosition;
    private float timer;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (recordedPositions.Count > 0)
        {
            transform.position = recordedPositions[0];
            lastPosition = recordedPositions[0];
        }
    }

    void Update()
    {
        if (recordedPositions.Count < 2 || currentIndex >= recordedPositions.Count)
            return;

        timer += Time.deltaTime * playbackSpeed;

        if (timer >= 0.02f)
        {
            timer = 0f;

            Vector2 currentPosition = recordedPositions[currentIndex];
            transform.position = currentPosition;

            AnimateFromMovement(currentPosition);

            lastPosition = currentPosition;
            currentIndex++;
        }
    }

    void AnimateFromMovement(Vector2 currentPosition)
    {
        Vector2 delta = currentPosition - lastPosition;

        float horizontalSpeed = Mathf.Abs(delta.x) / Time.deltaTime;
        float verticalSpeed = delta.y / Time.deltaTime;

        animator.SetFloat("Running", horizontalSpeed);


        bool isJumping = verticalSpeed > jumpThreshold;
        bool isFalling = verticalSpeed < -jumpThreshold;

        animator.SetBool("Jumping", isJumping);
        animator.SetBool("Jumping", isFalling);


        if (Mathf.Abs(delta.x) > 0.001f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(delta.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
