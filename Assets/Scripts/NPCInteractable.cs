using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    public Dialogue dialogueInitial;
    public Dialogue[] dialogueSpeak; 
    public Dialogue dialogueGiveQuest, dialogueQuestNotFinished, dialogueQuestFinished;

    private QuestGiver questGiver;

    void Awake()
    {
        questGiver = GetComponent<QuestGiver>();
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

    public override void Interact()
    {
        base.Interact();

        TriggerDialogue(dialogueInitial);
    }

    public void OnSpeakClick()
    {
        int index = Random.Range(0, dialogueSpeak.Length);

        TriggerDialogue(dialogueSpeak[index]);
    }

    public void OnQuestClick()
    {
        Quest quest = questGiver.quest;
        if (!quest.isActive && !quest.isComplete)
        {
            TriggerDialogue(dialogueGiveQuest);
            questGiver.AcceptQuest();
        }
        else if (!quest.isComplete)
        {
            TriggerDialogue(dialogueQuestNotFinished);
        }
        else if (quest.isComplete)
        {
            TriggerDialogue(dialogueQuestFinished);
            if (!quest.isRewarded)
                quest.RewardPlayer();
        }
    }
}
