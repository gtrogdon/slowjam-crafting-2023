﻿using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform InventorySlots;
    public GameObject Inventory;

    InventoryManager inventoryManager;
    InventorySlot[] slots;

    // Use this for initialization
    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        inventoryManager.OnItemChangedCallback += UpdateUI;

        slots = InventorySlots.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryManager.InventoryItems.Count)
            {
                slots[i].AddItem(inventoryManager.InventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void toggleInventoryUI()
    {
        Inventory.SetActive(!Inventory.activeSelf);
    }
}