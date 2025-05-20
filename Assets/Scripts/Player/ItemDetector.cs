using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    [SerializeField] Transform cameraContainer;
    [SerializeField] float distanceRange;
    [SerializeField] LayerMask layerMask;
    FoodInfo itemInfoUI;

    Food externalFood = null;
    Food inventoryFood = null;

    private void Start()
    {
        itemInfoUI = GameManager.Instance.UIManager.ItemInfo;
    }

    private void Update()
    {
        Ray ray = new Ray(cameraContainer.position, cameraContainer.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distanceRange, layerMask))
        {
            externalFood = hit.transform.GetComponent<Food>();
            itemInfoUI.ShowFoodInfo(externalFood.FoodData.FoodName, externalFood.FoodData.Description);
        }
        else
        {
            externalFood = null;
            itemInfoUI.HideFoodInfo();
        }
    }

    #region InputSystem
    // 상호작용(F)
    public void OnInteraction()
    {
        if(externalFood != null)
        {
            // 인벤토리에 음식이 있다면, 바깥에 생성
            if(inventoryFood != null)
            {
                Instantiate(inventoryFood.FoodData.DropPrefab, cameraContainer.transform.position + transform.forward, Quaternion.identity);
                Destroy(inventoryFood.gameObject);
            }

            // 인벤토리에 음식 오브젝트 생성
            inventoryFood = Instantiate(externalFood.FoodData.InventoryPrefab, cameraContainer.transform).GetComponent<Food>();
            Destroy(externalFood.gameObject);
            externalFood = null;
        }
        else
        {
            if (inventoryFood != null)
            {
                inventoryFood.Eat(GameManager.Instance.PlayerInfo);
                Destroy(inventoryFood.gameObject);
                inventoryFood = null;
                return;
            }
        }
    }
    #endregion
}
