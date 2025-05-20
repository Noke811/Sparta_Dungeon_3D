using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] Text itemName;
    [SerializeField] Text itemDesc;

    public void ShowItemInfo(string name, string desc)
    {
        itemName.text = name;
        itemDesc.text = desc;

        gameObject.SetActive(true);
    }

    public void HideItemInfo()
    {
        gameObject.SetActive(false);
    }
}
