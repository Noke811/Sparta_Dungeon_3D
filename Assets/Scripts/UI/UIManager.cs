using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] BarHandler hpBar;
    [SerializeField] BarHandler staminaBar;
    [SerializeField] ItemInfoUI itemInfoUI;
    public ItemInfoUI ItemInfo => itemInfoUI;

    private void Update()
    {
        float curHP = GameManager.Instance.PlayerInfo.Health;
        float maxHP = GameManager.Instance.PlayerInfo.MaxHealth;
        float curStamina = GameManager.Instance.PlayerInfo.Stamina;
        float maxStamina = GameManager.Instance.PlayerInfo.MaxStamina;

        hpBar.UpdateBar(curHP, maxHP);
        staminaBar.UpdateBar(curStamina, maxStamina);
    }
}
