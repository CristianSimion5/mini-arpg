using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeBtn;

    Item item;

    public void AddItem(Item addedItem)
    {
        item = addedItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeBtn.interactable = true;
    }

    public void RemoveItem()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeBtn.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
