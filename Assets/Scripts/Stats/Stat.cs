using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> bonuses = new List<int>();
    
    public int GetValue()
    {
        int total = baseValue;
        bonuses.ForEach(x => total += x);
        return total;
    }

    public void AddBonus(int bonus)
    {
        if (bonus != 0)
            bonuses.Add(bonus);
    }

    public void RemoveBonus(int bonus)
    {
        if (bonus != 0)
            bonuses.Remove(bonus);
    }
}
