using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    public LayerMask walkableMask;

    Camera cam;
    PlayerMover mover;
    public Interactable currentFocus;

    private Quaternion rotationTarget;
    private bool shouldRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        mover = GetComponent<PlayerMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, walkableMask))
            {
                // Debug.Log(hit.collider.ToString() + hit.point);
                mover.Move(hit.point);

                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Debug.Log(hit.collider.ToString() + hit.point);
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }


        if (shouldRotate)
            RotateToTarget();

        if (Input.GetButtonDown("Spell"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, walkableMask))
            {
                shouldRotate = true;
                rotationTarget = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
            }
            GetComponentInChildren<Animator>().SetTrigger("spellTrigger");
        }
    }

    void RotateToTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * 5f);
        if (Quaternion.Angle(transform.rotation, rotationTarget) < 15f)
            shouldRotate = false;
    }

    void RemoveFocus()
    {
        if (currentFocus != null)
            currentFocus.OnDefocus();
        currentFocus = null;
        mover.StopFollowing();
    }

    void SetFocus(Interactable focusObject)
    {
        if (focusObject != currentFocus)
        {
            if (currentFocus != null)
                currentFocus.OnDefocus();
            currentFocus = focusObject;
            mover.FollowTarget(focusObject);
        }

        focusObject.OnFocus(transform);
    }
}
