using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(GenericStats))]
public class Combat : MonoBehaviour
{
    private float attackCD = 0f;
    private float spellDamage = 30f;


    GenericStats selfStats;
    GenericStats savedStats;

    private void Start()
    {
        selfStats = GetComponentInParent<GenericStats>();
    }

    private void Update()
    {
        attackCD -= Time.deltaTime;
    }

    public void Attack(GenericStats opponentStats, Animator animator = null)
    {
        if (attackCD <= 0f)
        {
            attackCD = 1f / selfStats.attackSpeed.GetValue();
            if (animator != null)
            {
                animator.SetTrigger("attack");
            }
            savedStats = opponentStats;
        }
    }

    public void SpellAttack(GenericStats opponentStats, Animator animator)
    {
        opponentStats.TakeDamage(spellDamage);
    }

    public void DelayedPunch()
    {
        savedStats.TakeDamage(selfStats.attack.GetValue());
    }
}
