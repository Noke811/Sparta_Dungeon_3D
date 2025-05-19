using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] BarHandler hpBar;
    [SerializeField] BarHandler staminaBar;
    [SerializeField] PlayerInfo playerInfo;

    private void Update()
    {
        hpBar.UpdateBar(playerInfo.Health, playerInfo.MaxHealth);
        staminaBar.UpdateBar(playerInfo.Stamina, playerInfo.MaxStamina);
    }
}
