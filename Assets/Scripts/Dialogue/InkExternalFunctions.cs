using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("openShopUI", () => OpenShopUI());
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("openShopUI");
    }

    private void OpenShopUI()
    {
        ShopManager.Instance.toggleShopUI();
    }
}
