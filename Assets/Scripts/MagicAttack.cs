using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagicAttack : MonoBehaviour
{
    public Transform attackOrigin;
    public LayerMask enemyLayer;
    public GameObject particles;

    public Animator animator;
    private Combat playerCombat;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GetComponentInChildren<Combat>();
        agent = PlayerManager.instance.player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartAttack()
    {
        GameObject go = Instantiate(particles, attackOrigin);
        Destroy(go, particles.GetComponent<ParticleSystem>().main.duration);
        Collider[] colliders = Physics.OverlapBox(attackOrigin.position, new Vector3(2, 1, 1), attackOrigin.rotation, enemyLayer);

        foreach (var collider in colliders)
        {
            Debug.Log(collider);
            EnemyStats enemyStats = collider.gameObject.GetComponent<EnemyStats>();
            playerCombat.SpellAttack(enemyStats, animator);
        }

        agent.isStopped = false;
        agent.ResetPath();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(attackOrigin.position, new Vector3(4, 2, 2));
    }
}
