using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform pivot;
    Food inventoryFood = null;

    public void SaveFood(FoodData foodData, Transform dropPos)
    {
        if(inventoryFood != null)
        {
            TakeOutFood(dropPos);
        }

        inventoryFood = Instantiate(foodData.DropPrefab, pivot).GetComponent<Food>();
    }

    public void TakeOutFood(Transform dropPos)
    {
        Instantiate(inventoryFood.FoodData.DropPrefab, dropPos.position + transform.forward, Quaternion.identity);
        Destroy(inventoryFood.gameObject);
        inventoryFood = null;
    }

    public void EatFood()
    {
        if (inventoryFood == null) return;

        inventoryFood.Eat(GameManager.Instance.PlayerInfo);
        Destroy(inventoryFood.gameObject);
        inventoryFood = null;
    }
}
