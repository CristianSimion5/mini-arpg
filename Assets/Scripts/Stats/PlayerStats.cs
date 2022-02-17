using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : GenericStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChange += OnEquipmentChanged;
    }

    void OnEquipmentChanged(EquipableItem newItem, EquipableItem oldItem)
    {
        if (newItem != null)
        {
            attack.AddBonus(newItem.attack);
            defense.AddBonus(newItem.defense);
        }

        if (oldItem != null)
        {
            attack.RemoveBonus(oldItem.attack);
            defense.RemoveBonus(oldItem.defense);
        }
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.instance.DeathHandler();
    }
}
