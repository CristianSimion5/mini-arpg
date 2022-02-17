using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : GenericStats
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);

        Quest quest = PlayerManager.instance.quest;

        if (quest.isActive)
        {
            quest.goal.EnemyKilled();
            if (quest.goal.IsDone())
            {
                quest.Complete();
            }    
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        animator.SetTrigger("hit");
    }
}
