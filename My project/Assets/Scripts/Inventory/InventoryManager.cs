using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int maxCount = 24;
    private List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(InventoryItem item) 
    {
        if (items.Count == maxCount) 
        {
            return;
        }

        items.Add(item);
    }

    public void RemoveItem(string name)
    {
        var item = this.items.Find(item => item.Name == name);
        if (item is null) 
        {
            return;
        }

        this.items = this.items.Where(item => item.Name != name).ToList();
    }
    public IEnumerable<InventoryItem> GetAll() 
    {
        return this.items;
    }
}
