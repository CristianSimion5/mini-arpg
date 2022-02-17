using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 7f;
    public float sightRange = 11f;

    private bool chasingPlayer = false;

    private Vector3 walkPoint;
    private bool walkPointSet = false;
    public float walkPointRange = 5f;

    public GameObject canvas;
    private Animator animator;

    Transform target;
    NavMeshAgent agent;
    Combat enemyCombat;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<Combat>();
        animator = GetComponent<Animator>();

        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        bool canDetect = dist < detectionRange;
        bool canSee = dist < sightRange;

        if (canDetect)
        {
            agent.stoppingDistance = 1.5f;
            agent.speed = 3f;

            animator.SetBool("chasing", true);
            animator.SetBool("walking", false);
            agent.destination = target.position;
            chasingPlayer = true;
            canvas.SetActive(true);

            if (dist <= agent.stoppingDistance)
            {
                FaceTarget();

                GenericStats targetStats = target.GetComponent<GenericStats>();
                if (targetStats != null)
                {
                    animator.SetBool("chasing", false);
                    enemyCombat.Attack(targetStats, animator);
                }
            }
        }
        if (!canDetect && !canSee && chasingPlayer)
        {
            chasingPlayer = false;
            animator.SetBool("chasing", false);
            canvas.SetActive(false);
        }
        if (!chasingPlayer)
            Patrolling();
    }

    private void Patrolling()
    {
        agent.stoppingDistance = 0f;
        agent.speed = 0.5f;

        animator.SetBool("walking", true);
        if (!walkPointSet)
        {
            float x = Random.Range(-walkPointRange, walkPointRange);
            float z = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = transform.position + new Vector3(x, 0, z);
            walkPointSet = true;
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        float distToWalkPoint = Vector3.Distance(transform.position, walkPoint);

        if (distToWalkPoint < 1f)
            walkPointSet = false;
    }

    void FaceTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
