using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    public ItemSO Item;

    InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    public void CollectItem()
    {
        bool collected = inventoryManager.Add(Item);
        if (collected)
        {
            Debug.Log("Item Collected");
            AudioManager.instance.PlayOneShot(FMODEvents.instance.itemPickup, this.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.CollectItem();
        }
    }
}
