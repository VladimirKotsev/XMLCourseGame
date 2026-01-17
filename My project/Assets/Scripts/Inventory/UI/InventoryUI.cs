using System;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private const int maxRowCount = 8;

    private UIManager uiManager;
    private InventoryManager inventoryManager;
    private InventoryToggle inventoryToggle;
    private bool isInventoryOpen = false;

    public GameObject[] itemsContainers;
    public GameObject itemPrefab;   
    public GameObject uiItemElement;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        inventoryToggle = GameObject.FindGameObjectWithTag("UIManager").GetComponent<InventoryToggle>();
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInventoryOpen)
            {
                uiManager.State = UIState.Crosshair;
                this.isInventoryOpen = false;
                this.ClearItems();
                this.inventoryToggle.ToggleInventory();
            }
            else
            {
                uiManager.State = UIState.Inventory;
                this.isInventoryOpen = true;
                this.RenderItems();
                this.inventoryToggle.ToggleInventory();
            }
        }
    }

    public void RenderItems()
    {
        var items = inventoryManager.GetAll();

        const float startX = 0f;
        const float startY = 0f;
        const float xSpacing = 120f;
        const float ySpacing = 140f;

        for (int i = 0; i < items.Count(); i++)
        {
            // Calculate row & column
            int column = i % maxRowCount;
            int row = i / maxRowCount;

            float xPos = startX + (column * xSpacing);
            float yPos = startY - (row * ySpacing);

            foreach (var itemsContainer in itemsContainers)
            {
                GameObject itemGO = Instantiate(itemPrefab, itemsContainer.transform);

                RectTransform rect = itemGO.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(xPos, yPos);

                UIItem itemUI = itemGO.GetComponent<UIItem>();
                if (itemUI != null)
                {
                    itemUI.Name = items.ToArray()[i].Name;
                    itemUI.Description = items.ToArray()[i].Description;
                    itemUI.IconName = items.ToArray()[i].Icon;
                }
            }
        }
    }

    public void ClearItems()
    {
        foreach (var itemsContainer in itemsContainers) 
        {
            for (int i = itemsContainer.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(itemsContainer.transform.GetChild(i).gameObject);
            }
        }
    }

    public void ShowItem(InventoryItem inventoryItem) 
    {
        var item = uiItemElement.GetComponent<UIItem>();
        if (item != null) 
        {
            item.Name = inventoryItem.Name;
            item.Description = inventoryItem.Description;
            item.IconName = inventoryItem.Icon;
            item.UpdateMetaData();
        }
        this.uiItemElement.SetActive(true);
    }

    public void HideItem() 
    {
        this.uiItemElement.SetActive(false);
    }
}
