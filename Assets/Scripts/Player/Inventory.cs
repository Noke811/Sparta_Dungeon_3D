using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform pivot;
    Food inventoryFood = null;

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

    private void TakeOutFood(Transform dropPos)
    {
        inventoryFood.GetComponent<Rigidbody>().useGravity = true;
        inventoryFood.GetComponent<Collider>().enabled = true;

        inventoryFood.transform.SetParent(null);
        inventoryFood.transform.position = dropPos.position + dropPos.forward;
        inventoryFood.transform.localRotation = Quaternion.identity;

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
