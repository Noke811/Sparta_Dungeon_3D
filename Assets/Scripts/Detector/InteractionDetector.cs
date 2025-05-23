using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [Header("Detect")]
    [SerializeField] Transform cameraContainer;
    [SerializeField] float distanceRange;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform interactionPanel;
    [SerializeField] float upFactor;

    Inventory inventory;
    Food externalFood = null;
    public Food ExternalFood => externalFood;

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
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
        }
        else
        {
            HideInteractionKey();

            externalFood = null;
        }
    }

    // 해당 오브젝트 위에 상호작용 패널 띄우기
    private void ShowInteractionKey(Transform obj)
    {
        Collider collider = obj.GetComponent<Collider>();
        Vector3 newPos = new Vector3(collider.bounds.center.x, collider.bounds.max.y, collider.bounds.center.z);

        interactionPanel.position = newPos + Vector3.up * upFactor;
        interactionPanel.forward = interactionPanel.position - cameraContainer.position;
        interactionPanel.gameObject.SetActive(true);
    }

    // 상호작용 패널 숨기기
    private void HideInteractionKey()
    {
        interactionPanel.gameObject.SetActive(false);
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
