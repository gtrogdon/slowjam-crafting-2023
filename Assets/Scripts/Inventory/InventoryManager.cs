using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public Inventory Inventory;
    public ItemSO[] TestItems; // Test

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    private InventoryUI UI;
    int maxItems = 6;

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
        InitializeTestItems();
    }

    void InitializeTestItems()
    {
        if (TestItems.Length > 0)
        {
            foreach (ItemSO item in TestItems)
            {
                Add(item);
            }
        }
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

    public void Decrement(string name, int count)
    {
        int index = SearchForItemByName(name);
        InventoryItem inventoryItem = Inventory.InventoryItems[index];
        if (index > -1)
        {
            inventoryItem.Count -= count;
            if (inventoryItem.Count <= 0)
            {
                Inventory.InventoryItems.RemoveAt(index);
            }
        }
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public void Decrement(InventoryItem inventoryItem)
    {
        Decrement(inventoryItem, 1);
    }

    public void Decrement(InventoryItem inventoryItem, int count)
    {
        int index = SearchForItem(inventoryItem.Item);
        if (index > -1)
        {
            inventoryItem.Count -= count;
            if (inventoryItem.Count <= 0)
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

    public int SearchForItemByName(string itemName)
    {
        for (int i = 0; i < Inventory.InventoryItems.Count; i++)
        {
            if (Inventory.InventoryItems[i].Item.Name == itemName)
            {
                return i;
            }
        }
        return -1;
    }

    public void toggleInventoryUI() {
        UI.toggleInventoryUI();
    }

    public int GetDookieCount()
    {
        int index = SearchForItemByName("Dookie");
        if (index >= 0)
        {
            return Inventory.InventoryItems[index].Count;
        }
        return 0;
    }

    public void DecrementDookieCount(int countChange)
    {
        Decrement("Dookie", countChange);
    }
}
