using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "Unnamed item";
    public Sprite icon = null;
    public bool isDefault = false;

    public virtual void Use()
    {
        Debug.Log("Use: " + name);
    }

    public void Remove()
    {
        Inventory.instance.Remove(this);
    }
}
