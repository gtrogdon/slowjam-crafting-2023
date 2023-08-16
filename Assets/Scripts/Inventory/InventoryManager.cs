using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<InventoryItem> InventoryItems = new List<InventoryItem>();
    int maxItems = 4;

    void Awake()
    {
        if (Instance != null && Instance !=this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
     }

    public bool Add(ItemSO item)
    {
        if (item.Type == ItemType.TOOL)
        {
            if (InventoryItems.Count == maxItems)
            {
                Debug.Log("Inventory is Full");
                return false;
            }
            InventoryItems.Add(new InventoryItem(item, 1));
        }
        else
        {
            int index = SearchForItem(item);
            if (index > -1)
            {
                InventoryItems[index].AddItem(1);
            }
            else
            {
                if (InventoryItems.Count == maxItems)
                {
                    Debug.Log("Inventory is Full");
                    return false;
                }
                InventoryItems.Add(new InventoryItem(item, 1));
            }
        }

        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(InventoryItem inventoryItem)
    {
        InventoryItems.Remove(inventoryItem);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public void Decrement(InventoryItem inventoryItem)
    {
        int index = SearchForItem(inventoryItem.Item);
        if (index > -1)
        {
            if (inventoryItem.Count > 1)
            {
                InventoryItems[index].Count--;
            }
            else
            {
                InventoryItems.RemoveAt(index);
            }
        }
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public int SearchForItem(ItemSO item)
    {
        for (int i = 0; i < InventoryItems.Count; i++)
        {
            if (InventoryItems[i] != null && InventoryItems[i].Item.name == item.name)
            {
                return i;
            }
        }
        return -1;
    }
}
