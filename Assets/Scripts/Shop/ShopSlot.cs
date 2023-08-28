using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public ItemSO item;
    public int index;

    public GameObject SlotIcon;
    public Image SlotImage;
    public TMP_Text PriceText;

    public void SetSlot(ItemSO itemSO, int i)
    {
        index = i;
        item = itemSO;
        if (item)
        {
            SlotImage.sprite = item.Icon;
            PriceText.text = "D " + item.Price.ToString();
            SlotIcon.SetActive(true);
        }
    }

    public void ClearSlot()
    {
        item = null;
        if (SlotImage?.sprite != null)
        {
            SlotImage.sprite = null;
        }
        SlotIcon.SetActive(false);
        PriceText.text = "D ???";
        index = -1;
    }

    public void SelectItem()
    {
        if (index >= 0)
        {
            ShopManager.Instance.SelectItem(index);
        }
    }
}
