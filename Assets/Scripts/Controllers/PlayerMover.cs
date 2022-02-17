using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMover : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceFocusTarget();
        }
    }

    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void FollowTarget(Interactable targetToFollow)
    {
        agent.stoppingDistance = targetToFollow.radius * .9f;
        agent.updateRotation = false;

        target = targetToFollow.interactionTransform;
    }

    public void StopFollowing()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    void FaceFocusTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
