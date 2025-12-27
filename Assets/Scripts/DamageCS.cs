using UnityEngine;

public class DamageCS : MonoBehaviour
{
    public PlayerScript player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        player.playerNumbers.playerSouls -= 1;
        player.GetHit();
    }
}
