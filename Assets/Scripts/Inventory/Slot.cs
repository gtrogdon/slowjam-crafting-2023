using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public GameObject Icon;
    public Button RemoveButton;
    public Button SlotButton;
    public GameObject Count;
    public TMP_Text CountText;
    public InventoryItem inventoryItem { get; private set; }
    Image image;

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
        Count.SetActive(false);
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

    public virtual void OnRemoveButton()
    {
        Debug.Log("Replace: Clearing slot");
    }


    public virtual void OnSlotButton()
    {
        Debug.Log("Replace: Slot clicked");
    }
}
