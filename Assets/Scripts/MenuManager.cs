using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject creditsPanel;

    public void ToggleCreditsUI()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }


}
