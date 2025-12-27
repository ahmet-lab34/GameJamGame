using System.Collections;
using System.Drawing;
using UnityEngine;

public class EnemyCS : MonoBehaviour
{
    public GameObject Soul;
    public int enemyHealth = 5;
    
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private bool wait = false;

    private Transform target;

    void Start()
    {
        target = pointA;
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(Soul, transform.position, transform.rotation);
        }

        transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f && !wait)
        {
            StartCoroutine(Wait());
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    IEnumerator Wait()
    {
        wait = true;

        yield return new WaitForSeconds(2);

        target = target == pointA ? pointB : pointA;
        Flip();

        wait = false;
    }
}

