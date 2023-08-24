/* Slots for Inventory Panel during Crafting */
public class CraftInventorySlot : InventorySlot
{
    public override void OnSlotButton()
    {
        CraftingManager.Instance.AddIngredient(inventoryItem.Item);
        InventoryManager.Instance.Decrement(inventoryItem);
    }
}
