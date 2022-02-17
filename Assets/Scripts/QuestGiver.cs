using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public void AcceptQuest()
    {
        quest.isActive = true;
        PlayerManager.instance.quest = quest;
    }
}
