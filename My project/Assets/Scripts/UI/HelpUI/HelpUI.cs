using System;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    private UIManager uiManager;
    private bool isHelpOpen = false;

    // Drag your "HelpMenuPanel" (the black box with text) here in the Inspector
    public GameObject helpMenuContainer;

    void Start()
    {
        // Find the UIManager just like in your InventoryUI
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        // Ensure menu is closed on start
        if (helpMenuContainer != null)
        {
            helpMenuContainer.SetActive(false);
        }
    }

    void Update()
    {
        // Toggle on Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isHelpOpen)
            {
                CloseHelpMenu();
            }
            else
            {
                OpenHelpMenu();
            }
        }
    }

    private void OpenHelpMenu()
    {
        // 1. Switch UI State to unlock cursor (Reusing 'Inventory' state or add 'Help' to your Enum)
        // If your UIManager handles cursor unlocking, this is all you need.
        uiManager.State = UIState.Help;

        // 2. Set boolean flag
        isHelpOpen = true;

        // 3. Show the visual panel
        if (helpMenuContainer != null)
            helpMenuContainer.SetActive(true);
    }

    private void CloseHelpMenu()
    {
        // 1. Switch UI State back to Crosshair (locks cursor, enables shooting)
        uiManager.State = UIState.Crosshair;

        // 2. Set boolean flag
        isHelpOpen = false;

        // 3. Hide the visual panel
        if (helpMenuContainer != null)
            helpMenuContainer.SetActive(false);
    }
}