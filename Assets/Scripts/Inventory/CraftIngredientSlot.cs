/* Slots on Crafting Panel, Currently Selected Ingredients */
public class CraftIngredientSlot : Slot
{
    public override void OnRemoveButton()
    {
        InventoryManager.Instance.Add(inventoryItem.Item);
        CraftingManager.Instance.RemoveIngredient(inventoryItem.Item);
    }
}
