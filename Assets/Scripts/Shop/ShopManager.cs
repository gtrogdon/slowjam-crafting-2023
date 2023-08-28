using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    private ShopUI UI;
    private string defaultText = "Let me know what you\'re interested in. (Select an item)";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<ShopUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleShopUI()
    {
        UI.toggleShopUI();
    }
}
