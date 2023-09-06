using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public Transform SlotParent;
    public GameObject Crafting;
    public Image ResultIcon;
    public Button CraftButton;

    private CraftingManager craftingManager;
    Slot[] slots;

    void Start()
    {
        craftingManager = CraftingManager.Instance;
        craftingManager.OnItemChangedCallback += UpdateIngredientsUI;
        craftingManager.OnRecipeChangedCallback += UpdateOutputUI;

        slots = SlotParent.GetComponentsInChildren<Slot>();

        // Initial update for if this is being loaded after a scene transition,
        // otherwise inventory appears empty.
        UpdateIngredientsUI();
        UpdateOutputUI();
    }

    void UpdateIngredientsUI()
    {
        if (craftingManager.currentIngredients != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < craftingManager?.currentIngredients.Count)
                {
                    slots[i].AddItem(new InventoryItem(craftingManager.currentIngredients[i], 1));
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }

    void UpdateOutputUI()
    {
        if (craftingManager.currentRecipe)
        {
            ResultIcon.sprite = craftingManager.currentRecipe.OutputItem.Icon;
            ResultIcon.gameObject.SetActive(true);
            CraftButton.interactable = true;
        }
        else
        {
            ResultIcon.sprite = null;
            ResultIcon.gameObject.SetActive(false);
            CraftButton.interactable = false;
        }
    }

    public void toggleCraftingUI()
    {
        Crafting.SetActive(!Crafting.activeSelf);
    }
}
