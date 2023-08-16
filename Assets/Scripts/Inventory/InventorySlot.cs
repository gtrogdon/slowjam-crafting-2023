using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public GameObject Icon;
    public Button RemoveButton;
    public Button SlotButton;
    public GameObject Count;
    public TMP_Text CountText;
    Image image;

    InventoryItem inventoryItem;

    void Start()
    {
        image = Icon.GetComponent<Image>();
    }

    public void AddItem(InventoryItem newItem)
    {
        inventoryItem = newItem;
        if (inventoryItem.Count > 1)
        {
            CountText.text = inventoryItem.Count.ToString();
            Count.SetActive(true);
        }
        else
        {
            Count.SetActive(false);
        }
        if (setImage())
        {
            image.sprite = inventoryItem.Item.Icon;
        }
        else {
            Debug.LogWarning("Couldn't Set Inventory Slot");
        }
        Icon.SetActive(true);
        RemoveButton.interactable = true;
        SlotButton.interactable = true;
    }

    public void ClearSlot()
    {
        inventoryItem = null;
        if (image != null && image?.sprite != null)
        {
            image.sprite = null;
        }
        Icon.SetActive(false);
        RemoveButton.interactable = false;
        SlotButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Debug.Log("clearing slot");
        InventoryManager.Instance.Remove(inventoryItem);
    }

    private bool setImage()
    {
        if (image != null)
        {
            return true;
        }
        Debug.Log("Getting Slot Image..");
        image = Icon.GetComponent<Image>();
        return image != null;
    }

    public void OnSlotButton()
    {
        Debug.Log("Button Pressed");
        inventoryItem.Item.Use();
        InventoryManager.Instance.Decrement(inventoryItem);
    }
}
