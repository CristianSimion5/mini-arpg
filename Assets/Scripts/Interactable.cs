using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocused = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
         // Debug.Log("Interactiune: " + transform.name);
    }

    private void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float dist = Vector3.Distance(player.position, interactionTransform.position);
            if (dist <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocus(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocus()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
