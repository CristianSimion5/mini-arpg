using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericStats : MonoBehaviour
{
    public float maxHp = 100;
    public float currentHp { get; private set; }

    public Slider hpBar;

    public Stat attack;
    public Stat defense;
    public Stat attackSpeed;
    public Stat attackRange;

    private void Awake()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
    }

    public virtual void TakeDamage(float damage)
    {
        float postMitigationDamage = damage / (1 + defense.GetValue() / 100f);
        currentHp -= postMitigationDamage;
        Debug.Log("Taking " + postMitigationDamage + " damage");

        if (hpBar != null)
        {
            hpBar.value = currentHp / maxHp * 100;
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        if (hpBar != null)
        {
            hpBar.value = currentHp / maxHp * 100;
        }
    }

    public void RemoveBuffAfterSeconds(int stat, int statVal, float time)
    {
        StartCoroutine(RemoveAttack(stat, statVal, time));
    }

    IEnumerator RemoveAttack(int stat, int statVal, float time)
    {
        yield return new WaitForSeconds(time);
        if (stat == 0)
            attack.RemoveBonus(statVal);
        if (stat == 1)
            defense.RemoveBonus(statVal);

        Debug.Log("Buff " + stat + " with strength " + statVal + " expired");
    }


    public virtual void Die()
    {
        Debug.Log(transform.name + " is wasted");
    }
}
