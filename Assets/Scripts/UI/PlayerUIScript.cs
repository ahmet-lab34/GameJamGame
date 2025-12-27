using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    PlayerScript player;
    [SerializeField] private TMP_Text playerSoulCondition;

    [SerializeField] private UIScript DeathPanelForPlayer;

    void Awake()
    {
        player = GetComponent<PlayerScript>();
        DeathPanelForPlayer = FindFirstObjectByType<UIScript>();
    }

    void Update()
    {
        playerSoulCondition.text = player.playerNumbers.playerSouls.ToString();
    }

    public void Die()
    {
        DeathPanelForPlayer.UIDie();
    }
}
