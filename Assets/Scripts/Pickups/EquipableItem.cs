using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New equipable item", menuName = "Inventory/Equipable Item")]
public class EquipableItem : Item
{
    public EquipmentSlot equipmentSlot;

    public int attack, defense;

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);
        Remove();
    }
}

public enum EquipmentSlot { Helmet, Chest, Gloves, Legs, Boots, Weapon };