using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isComplete;
    public bool isRewarded = false;

    public string title;
    public List<Item> rewards;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        isComplete = true;
        Debug.Log("The quest \"" + title + "\" has been completed!");
    }

    public void RewardPlayer()
    {
        isRewarded = true;

        foreach (Item reward in rewards)
        {
            Inventory.instance.Add(reward);
        }
    }
}
