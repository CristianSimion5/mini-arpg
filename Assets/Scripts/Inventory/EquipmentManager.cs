using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("EquipmentManager should be a singleton!");
            return;
        }

        instance = this;
    }

    #endregion

    public Animator animator;
    public GameObject sword, bow;

    public EquipableItem[] currentEquipment;

    public delegate void OnEquipmentChange(EquipableItem newItem, EquipableItem oldItem);
    public OnEquipmentChange onEquipmentChange;

    Inventory inventory;

    void Start()
    {
        int numEquipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new EquipableItem[numEquipSlots];
        
        inventory = Inventory.instance;

        onEquipmentChange += CheckWeaponStance;
    }

    void CheckWeaponStance(EquipableItem newItem, EquipableItem oldItem)
    {
        if (newItem == null)
        {
            animator.SetInteger("weaponStance", 0);
            sword.SetActive(false);
            bow.SetActive(false);
            return;
        }

        if (newItem.equipmentSlot != EquipmentSlot.Weapon)
            return;

        if (newItem.name != "Bow")
        {
            animator.SetInteger("weaponStance", 1);
            sword.SetActive(true);
            bow.SetActive(false);
            PlayerManager.instance.playerStats.attackRange.RemoveBonus(6);
        }
        else
        {
            animator.SetInteger("weaponStance", 2);
            bow.SetActive(true);
            sword.SetActive(false);
            PlayerManager.instance.playerStats.attackRange.AddBonus(6);
        }
    }

    public void Equip(EquipableItem item)
    {
        int slotIndex = (int) item.equipmentSlot;
        
        EquipableItem previousItemAtIndex = Unequip(slotIndex);

        if (onEquipmentChange != null)
        {
            onEquipmentChange.Invoke(item, previousItemAtIndex);
        }

        currentEquipment[slotIndex] = item;
    }

    public EquipableItem Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            EquipableItem item = currentEquipment[slotIndex];

            currentEquipment[slotIndex] = null;
            inventory.Add(item);

            if (onEquipmentChange != null)
            {
                onEquipmentChange.Invoke(null, item);
            }

            return item;
        }

        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
