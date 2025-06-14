using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] BarHandler hpBar;
    [SerializeField] BarHandler staminaBar;

    PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = GameManager.Instance.PlayerInfo;
    }

    private void Update()
    {
        hpBar.UpdateBar(playerInfo.Health, playerInfo.MaxHealth);
        staminaBar.UpdateBar(playerInfo.Stamina, playerInfo.MaxStamina);
    }
}
