using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : Interactable
{
    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.SetInteractable(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.SetInteractable(null);
    }
}
