using UnityEngine;
using UnityEngine.UI;

public class FoodInfo : MonoBehaviour
{
    [SerializeField] GameObject foodInfoPanel;
    [SerializeField] Text foodName;
    [SerializeField] Text foodDesc;

    InteractionDetector detector;
    Inventory inventory;
    Food displayFood;

    private void Start()
    {
        detector = GameManager.Instance.Player.GetComponent<InteractionDetector>();
        inventory = GameManager.Instance.Inventory;

        HideFoodInfo();
    }

    private void Update()
    {
        if(detector.ExternalFood != null)
        {
            displayFood = detector.ExternalFood;
        }
        else
        {
            if (inventory.InventoryFood != null)
            {
                displayFood = inventory.InventoryFood;
            }
            else displayFood = null;
        }

        if(displayFood != null)
        {
            FoodData data = displayFood.FoodData;
            ShowFoodInfo(data.FoodName, data.Description);
        }
        else
        {
            HideFoodInfo();
        }
    }

    private void ShowFoodInfo(string name, string desc)
    {
        foodName.text = name;
        foodDesc.text = desc;

        foodInfoPanel.SetActive(true);
    }

    private void HideFoodInfo()
    {
        foodInfoPanel.SetActive(false);
    }
}
