using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] Inventory inventory;

    [Header("Detect")]
    [SerializeField] Transform cameraContainer;
    [SerializeField] float distanceRange;
    [SerializeField] LayerMask layerMask;
    FoodInfo foodInfo;

    Food externalFood = null;

    private void Start()
    {
        foodInfo = GameManager.Instance.UIManager.FoodInfo;
    }

    private void Update()
    {
        Ray ray = new Ray(cameraContainer.position, cameraContainer.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distanceRange, layerMask))
        {
            externalFood = hit.transform.GetComponent<Food>();
            foodInfo.ShowFoodInfo(externalFood.FoodData.FoodName, externalFood.FoodData.Description);
        }
        else
        {
            externalFood = null;
            foodInfo.HideFoodInfo();
        }
    }

    #region InputSystem
    // 상호작용(F)
    void OnInteraction()
    {
        if(externalFood != null)
        {
            inventory.SaveFood(externalFood, cameraContainer);
            externalFood = null;
        }
        else
        {
            inventory.EatFood();
        }
    }
    #endregion
}
