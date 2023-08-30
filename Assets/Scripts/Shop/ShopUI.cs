using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopPanel;
    public Transform ShopItemsSlots;

    public GameObject Dialogue;
    public TMP_Text DialogueText;
    public TMP_Text DookieCount;
    public GameObject ItemInfo;
    public TMP_Text SelectedItemName;
    public TMP_Text SelectedItemDesc;
    public Image SelectedIcon;


    private ShopManager shopManager;
    private InventoryManager inventoryManager;
    private ShopSlot[] slots = new ShopSlot[10];
    private string defaultText = "Let me know what you\'re interested in. (Select an item)";
    private string lessDookiesText = "... you don't have enough dookies...";
    private string noSpaceText = "... you don't have enough inventory space...";

    void Start()
    {
        shopManager = ShopManager.Instance;
        shopManager.OnItemChangedCallback += UpdateUI;
        shopManager.OnSelectedItemChangedCallback += UpdateSelectedItem;
        inventoryManager = InventoryManager.Instance;
        inventoryManager.OnItemChangedCallback += UpdateDookieCount;


        slots = ShopItemsSlots.GetComponentsInChildren<ShopSlot>();
        DialogueText.text = defaultText;

        // Initial update for if this is being loaded after a scene transition,
        // otherwise inventory appears empty.
        UpdateUI();
        UpdateDookieCount();
    }

    void UpdateDookieCount()
    {
        DookieCount.text = inventoryManager.GetDookieCount().ToString();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shopManager.items.Length)
            {
                slots[i].SetSlot(shopManager.items[i], i);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateSelectedItem()
    {
        if (ShopManager.Instance.currentItemIndex >= 0)
        {
            ItemSO item = ShopManager.Instance.items[ShopManager.Instance.currentItemIndex];
            SelectedItemName.text = item.Name;
            SelectedItemDesc.text = item.Description;
            SelectedIcon.sprite = item.Icon;
            ItemInfo.SetActive(true);
            Dialogue.SetActive(false);
        }
        else
        {
            ItemInfo.SetActive(false);
            Dialogue.SetActive(true);
        }    
    }

    public void toggleShopUI()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }

    public void BuyItem()
    {
        if (!shopManager.Buy())
        {
            if (inventoryManager.Inventory.InventoryItems.Count >= inventoryManager.maxItems)
            {
                DialogueText.text = noSpaceText;
            }
            else
            {
                DialogueText.text = lessDookiesText;
            }
            ItemInfo.SetActive(false);
            Dialogue.SetActive(true);
        }
    }
}
