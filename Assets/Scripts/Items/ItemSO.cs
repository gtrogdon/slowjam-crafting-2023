using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemType", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemType Type;
    public Sprite Icon;

    public virtual void Use()
    {
        Debug.Log("Item Used - replace this method");

    }
}

public enum ItemType
{
    TOOL,
    COMPONENT
}