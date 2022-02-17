using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Pickup " + item.name);
        bool pickupSuccess = Inventory.instance.Add(item);
        
        if (pickupSuccess)
            Destroy(gameObject);
    }
}
