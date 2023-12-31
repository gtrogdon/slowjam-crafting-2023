using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemType", order = 2)]
public class ItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemType Type;
    public Sprite Icon;
    public int Price = 1;

    public virtual void Use()
    {
        Debug.Log("Item Used - replace this method");

    }
}

public enum ItemType
{
    TOOL,
    COMPONENT,
    ANDROID_PART
}