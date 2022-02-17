using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Inventory should be a singleton!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    public int limit = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefault)
        {
            if (items.Count >= limit)
            {
                //Debug.Log("Not enough room");
                return false;
            }
            items.Add(item);

            if (onInventoryChangedCallback != null)
                onInventoryChangedCallback.Invoke();
        
            return true;
        }

        return false;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onInventoryChangedCallback != null)
            onInventoryChangedCallback.Invoke();
    }
}
