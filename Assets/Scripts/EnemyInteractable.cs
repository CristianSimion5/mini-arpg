using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GenericStats))]
public class EnemyInteractable : Interactable
{
    PlayerManager playerManager;
    GenericStats selfStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        selfStats = GetComponent<GenericStats>();
    }

    private void LateUpdate()
    {
        radius = playerManager.playerStats.attackRange.GetValue();
    }

    public override void Interact()
    {
        base.Interact();

        Combat playerCombat = playerManager.player.GetComponentInChildren<Combat>();
        Animator animator = playerManager.player.GetComponentInChildren<Animator>();

        if (animator.GetInteger("weaponStance") == 0)
            animator = null;

        if (playerCombat != null)
        {
            playerCombat.Attack(selfStats, animator);
        }
    }
}
