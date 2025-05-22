using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] Inventory inventory;

    [Header("Detect")]
    [SerializeField] Transform cameraContainer;
    [SerializeField] float distanceRange;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform interactionPanel;
    [SerializeField] float upFactor;
    FoodInfo foodInfo;

    Food externalFood = null;

    private void Start()
    {
        foodInfo = GameManager.Instance.UIManager.FoodInfo;
        interactionPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        Ray ray = new Ray(cameraContainer.position, cameraContainer.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distanceRange, layerMask))
        {
            ShowInteractionKey(hit.transform);

            externalFood = hit.transform.GetComponent<Food>();
            foodInfo.ShowFoodInfo(externalFood.FoodData.FoodName, externalFood.FoodData.Description);
        }
        else
        {
            interactionPanel.gameObject.SetActive(false);

            externalFood = null;
            foodInfo.HideFoodInfo();
        }
    }

    private void ShowInteractionKey(Transform obj)
    {
        Collider collider = obj.GetComponent<Collider>();
        Vector3 newPos = new Vector3(collider.bounds.center.x, collider.bounds.max.y, collider.bounds.center.z);

        interactionPanel.position = newPos + Vector3.up * upFactor;
        interactionPanel.forward = interactionPanel.position - cameraContainer.position;
        interactionPanel.gameObject.SetActive(true);
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
