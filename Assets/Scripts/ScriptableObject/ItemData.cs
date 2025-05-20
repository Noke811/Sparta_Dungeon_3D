using UnityEngine;



[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] string itemName;
    public string ItemName => itemName;
    [SerializeField] string description;
    public string Description => description;
    [SerializeField] Sprite icon;
    public Sprite Icon => icon;
    
}
