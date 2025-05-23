using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform pivot;
    Food inventoryFood = null;
    public Food InventoryFood => inventoryFood;

    // 인벤토리에 음식 저장
    public void SaveFood(Food food, Transform dropPos)
    {
        if(inventoryFood != null)
        {
            TakeOutFood(dropPos);
        }

        food.GetComponent<Rigidbody>().useGravity = false;
        food.GetComponent<Rigidbody>().velocity = Vector3.zero;
        food.GetComponent<Collider>().enabled = false;

        food.transform.SetParent(pivot);
        food.transform.localPosition = Vector3.zero;
        food.transform.localRotation = Quaternion.identity;

        inventoryFood = food;
    }

    // 인벤토리에 음식이 있을 때 음식 바깥으로 반환
    private void TakeOutFood(Transform dropPos)
    {
        inventoryFood.GetComponent<Rigidbody>().useGravity = true;
        inventoryFood.GetComponent<Collider>().enabled = true;

        inventoryFood.transform.SetParent(null);
        inventoryFood.transform.position = dropPos.position + dropPos.forward;
        inventoryFood.transform.localRotation = Quaternion.identity;

        inventoryFood = null;
    }

    // 인벤토리에 음식이 있을 때 음식 먹고 효과 적용
    public void EatFood()
    {
        if (inventoryFood == null) return;

        inventoryFood.Eat(GameManager.Instance.PlayerInfo);
        Destroy(inventoryFood.gameObject);
        inventoryFood = null;
    }
}
