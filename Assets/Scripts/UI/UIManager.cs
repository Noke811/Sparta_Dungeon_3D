using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] BarHandler hpBar;
    [SerializeField] BarHandler staminaBar;
    [SerializeField] FoodInfo itemInfoUI;
    public FoodInfo ItemInfo => itemInfoUI;

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
