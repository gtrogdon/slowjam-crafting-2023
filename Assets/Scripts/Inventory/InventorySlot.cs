/* Slots for Inventory UI, non-crafting mode */
public class InventorySlot : Slot
{
    public override void OnRemoveButton()
    {
        InventoryManager.Instance.Remove(inventoryItem);
    }

    public override void OnSlotButton()
    {
        inventoryItem.Item.Use();
        InventoryManager.Instance.Decrement(inventoryItem);
    }
}
