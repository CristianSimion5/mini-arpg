using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour
{
    public Image icon;

    EquipableItem item;

    public void AddItem(EquipableItem addedItem)
    {
        item = addedItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void RemoveItem()
    {
        if (item == null)
            return;
        
        EquipmentManager.instance.Unequip((int) item.equipmentSlot);

        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
