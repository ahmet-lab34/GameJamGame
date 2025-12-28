using System.Collections;
using UnityEngine;

public class SoulDropCS : MonoBehaviour
{
    private bool canBeDestroyed = false;
    private PlayerScript player;
    void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.playerNumbers.playerSouls < 5)
            {
                player.playerNumbers.playerSouls += 1;
            }
            Destroy(gameObject);
        }
        if (!canBeDestroyed)
        {
            StartCoroutine(soulDisappearing());
        }
    }
    IEnumerator soulDisappearing()
    {
        canBeDestroyed = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
