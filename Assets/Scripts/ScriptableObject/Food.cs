using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] FoodData foodData;
    public FoodData FoodData => foodData;

    public string GetFoodName() => foodData.FoodName;
    public string GetFoodDesc() => foodData.Description;

    public void Eat(PlayerInfo playerInfo)
    {
        switch (foodData.StatType)
        {
            case StatType.Health:
                playerInfo.Heal(foodData.StatValue);
                break;

            case StatType.Stamina:
                playerInfo.Rest(foodData.StatValue);
                break;

            case StatType.Speed:
                StartCoroutine(playerInfo.SpeedBuff(foodData.StatValue, foodData.Duration));
                break;

            case StatType.JumpPower:
                StartCoroutine(playerInfo.JumpBuff(foodData.StatValue, foodData.Duration));
                break;
        }
    }
}
