using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform InventorySlots;
    public GameObject InventoryPanel;

    InventoryManager inventoryManager;
    InventorySlot[] slots;

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        inventoryManager.OnItemChangedCallback += UpdateUI;

        slots = InventorySlots.GetComponentsInChildren<InventorySlot>();

        // Initial update for if this is being loaded after a scene transition,
        // otherwise inventory appears empty.
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryManager.Inventory.InventoryItems.Count)
            {
                slots[i].AddItem(inventoryManager.Inventory.InventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void toggleInventoryUI()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
    }
}
