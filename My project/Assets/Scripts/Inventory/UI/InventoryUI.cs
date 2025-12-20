using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private const int maxRowCount = 8;

    private UIManager uiManager;
    private InventoryManager inventoryManager;
    private bool isInventoryOpen = false;

    public GameObject itemsContainer;
    public GameObject itemPrefab;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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
            }
            else
            {
                uiManager.State = UIState.Inventory;
                this.isInventoryOpen = true;
                this.RenderItems();
            }
        }
    }

    private void RenderItems()
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

            GameObject itemGO = Instantiate(itemPrefab, itemsContainer.transform);

            RectTransform rect = itemGO.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(xPos, yPos);

            UIItem itemUI = itemGO.GetComponent<UIItem>();
            if (itemUI != null)
            {
                itemUI.Name = items.ToArray()[i].Name;
            }
        }
    }

    private void ClearItems()
    {
        for (int i = itemsContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(itemsContainer.transform.GetChild(i).gameObject);
        }
    }

}
