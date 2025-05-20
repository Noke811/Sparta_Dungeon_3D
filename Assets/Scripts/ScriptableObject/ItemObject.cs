using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemData itemData;

    public string GetItemName() => itemData.ItemName;
    public string GetItemDesc() => itemData.Description;
}
