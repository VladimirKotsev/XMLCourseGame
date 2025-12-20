using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int maxCount = 24;
    private List<InventoryItem> items = new List<InventoryItem>();

    public void Start()
    {
        // TODO: Remove these
        this.items.Add(new InventoryItem { Name="Item 1" });
        this.items.Add(new InventoryItem { Name="Item 2" });
        this.items.Add(new InventoryItem { Name="Item with long name" });
    }

    public void AddItem(InventoryItem item) 
    {
        if (items.Count == maxCount) 
        {
            return;
        }

        items.Add(item);
    }

    public IEnumerable<InventoryItem> GetAll() 
    {
        return this.items;
    }
}
