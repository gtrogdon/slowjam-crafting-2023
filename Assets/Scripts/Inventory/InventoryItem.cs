using System;

public class InventoryItem
{
    public int Count;
    public ItemSO Item;

    public InventoryItem(ItemSO item, int count)
    {
        Item = item;
        Count = count;
    }

    public void AddItem(int count)
    {
        Count += count;
    }

    public int RemoveItem(int count)
    {
        Count -= count;
        return Count;
    }
}
