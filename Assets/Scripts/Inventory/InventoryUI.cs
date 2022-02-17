using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform equipmentUI;
    public Transform itemsGrid;
    public GameObject inventoryPanel;
    Inventory inventory;

    EquippedSlot[] equippedSlots;
    InventorySlot[] inventorySlots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onInventoryChangedCallback += UpdateUI;

        equippedSlots = equipmentUI.GetComponentsInChildren<EquippedSlot>();
        inventorySlots = itemsGrid.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    void UpdateUI()
    {
        // Debug.Log("Update UI");
        for (int i = 0; i < equippedSlots.Length; i++)
        {
            EquipableItem item = EquipmentManager.instance.currentEquipment[i];
            if (item != null)
            {
                equippedSlots[i].AddItem(item);
            }
            else
            {
                equippedSlots[i].RemoveItem();
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                inventorySlots[i].RemoveItem();
            }
        }
    }
}
