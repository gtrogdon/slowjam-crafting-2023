using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public InventoryUI UI;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public Inventory Inventory;
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

     void Start() {
        UI = GetComponent<InventoryUI>();
     }

    public bool Add(ItemSO item)
    {
        if (item.Type == ItemType.TOOL)
        {
            if (Inventory.InventoryItems.Count == maxItems)
            {
                Debug.Log("Inventory is Full");
                return false;
            }
            Inventory.InventoryItems.Add(new InventoryItem(item, 1));
        }
        else
        {
            int index = SearchForItem(item);
            if (index > -1)
            {
                Inventory.InventoryItems[index].AddItem(1);
            }
            else
            {
                if (Inventory.InventoryItems.Count == maxItems)
                {
                    Debug.Log("Inventory is Full");
                    return false;
                }
                Inventory.InventoryItems.Add(new InventoryItem(item, 1));
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
        Inventory.InventoryItems.Remove(inventoryItem);
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
                Inventory.InventoryItems[index].Count--;
            }
            else
            {
                Inventory.InventoryItems.RemoveAt(index);
            }
        }
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public int SearchForItem(ItemSO item)
    {
        for (int i = 0; i < Inventory.InventoryItems.Count; i++)
        {
            if (Inventory.InventoryItems[i] != null && Inventory.InventoryItems[i].Item.name == item.name)
            {
                return i;
            }
        }
        return -1;
    }

    public void toggleInventoryUI() {
        UI.toggleInventoryUI();
    }
}
