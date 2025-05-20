using UnityEngine;

public enum StatType
{
    Health,
    Stamina,
    Speed,
    JumpPower,
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] string itemName;
    public string ItemName => itemName;
    [SerializeField] string description;
    public string Description => description;
    [SerializeField] GameObject dropPrefab;
    public GameObject DropPrefab => dropPrefab;
    [SerializeField] GameObject inventoryPrefab;
    public GameObject InventoryPrefab => inventoryPrefab;

    [Header("Effect")]
    [SerializeField] StatType statType;
    public StatType StatType => statType;
    [SerializeField] float statValue;
    public float StatValue => statValue;
    [SerializeField] bool hasDuration;
    public bool HasDuration => hasDuration;
    [SerializeField] float duration;
    public float Duration => duration;
}
