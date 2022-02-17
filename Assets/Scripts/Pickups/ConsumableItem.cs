using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class ConsumableItem : Item
{
    public ConsumableType type;
    public Stat bonusStat;

    public override void Use()
    {
        base.Use();

        int magnitude = bonusStat.GetValue();

        if (type == ConsumableType.Healing)
        {
            PlayerManager.instance.playerStats.Heal(magnitude);
        }
        else if (type == ConsumableType.BonusAttack)
        {
            PlayerManager.instance.playerStats.attack.AddBonus(magnitude);
            PlayerManager.instance.playerStats.RemoveBuffAfterSeconds(stat: 0, statVal: magnitude, time: 10f);
        }
        else if (type == ConsumableType.BonusDefense)
        {
            PlayerManager.instance.playerStats.defense.AddBonus(magnitude);
            PlayerManager.instance.playerStats.RemoveBuffAfterSeconds(stat: 1, statVal: magnitude,  time: 10f);
        }

        Remove();
    }
}

public enum ConsumableType { Healing, BonusAttack, BonusDefense };

