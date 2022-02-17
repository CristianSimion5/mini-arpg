using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goaltype;

    public int required;
    public int current;

    public bool IsDone()
    {
        return required <= current;
    }

    public void EnemyKilled()
    {
        if (goaltype == GoalType.Kill)
        {
            current++;
        }
    }
}

public enum GoalType { Kill, Speak, Collect }