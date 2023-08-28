using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    public ItemSO[] DefaultItems;
    public ItemSO[] items = new ItemSO[10];

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    public delegate void OnSelectedItemChanged();
    public OnSelectedItemChanged OnSelectedItemChangedCallback;
    public int currentItemIndex = -1;

    private ShopUI UI;
    private InventoryManager inventoryManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<ShopUI>();
        inventoryManager = InventoryManager.Instance;
        InitializeDefaultItems();
    }

    void InitializeDefaultItems()
    {
        if (DefaultItems.Length > 0)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (DefaultItems[i] != null)
                {
                    items[i] = DefaultItems[i];
                }
            }
            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
        }
    }

    public void toggleShopUI()
    {
        UI.toggleShopUI();
    }

    public void SelectItem(int index)
    {
        currentItemIndex = index;
        if (OnSelectedItemChangedCallback != null)
        {
            OnSelectedItemChangedCallback.Invoke();
        }
    }

    public bool Buy()
    {
        if (currentItemIndex >= 0)
        {
            ItemSO itemToBuy = items[currentItemIndex];
            int dookies = inventoryManager.GetDookieCount();
            if (itemToBuy.Price <= dookies)
            {
                bool successfulAdd = inventoryManager.Add(itemToBuy);
                if (successfulAdd)
                {
                    inventoryManager.DecrementDookieCount(itemToBuy.Price);
                    return true;
                }
            }
        }
        return false;
    }
}
