using UnityEngine;
using UnityEngine.UI;

public class FoodInfo : MonoBehaviour
{
    [SerializeField] Text foodName;
    [SerializeField] Text foodDesc;

    private void Start()
    {
        HideFoodInfo();
    }

    public void ShowFoodInfo(string name, string desc)
    {
        foodName.text = name;
        foodDesc.text = desc;

        gameObject.SetActive(true);
    }

    public void HideFoodInfo()
    {
        gameObject.SetActive(false);
    }
}
