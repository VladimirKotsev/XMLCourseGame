using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private UIManager uiManager;
    private bool isInventoryOpen = false;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInventoryOpen)
            {
                uiManager.State = UIState.Crosshair;
                this.isInventoryOpen = false;
            }
            else 
            {
                uiManager.State = UIState.Inventory;
                this.isInventoryOpen = true;
            }
        }
    }
}
