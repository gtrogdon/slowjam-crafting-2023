using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    //public Transform SlotParent;
    //public GameObject Crafting;
    //public Image ResultIcon;

    //private CraftingManager craftingManager;
    //Slot[] slots;

    public GameObject ShopPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleShopUI()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }
}
