using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    PlayerScript player;
    [SerializeField] private TMP_Text dashCooldown_Display;
    [SerializeField] private Slider dashCooldown_Visual;
    [SerializeField] private Slider playerHealthCondition;

    [SerializeField] private UIScript DeathPanelForPlayer;

    void Awake()
    {
        player = GetComponent<PlayerScript>();
        DeathPanelForPlayer = FindFirstObjectByType<UIScript>();
    }

    void Update()
    {
        playerHealthCondition.value = player.playerNumbers.playerHealth;
    }

    public void Die()
    {
        DeathPanelForPlayer.UIDie();
    }
}
