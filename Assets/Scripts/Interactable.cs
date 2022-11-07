using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void SetInteractable(Interactable interactable)
    {
        GameManager.Instance.currentInteractable = interactable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
            this.SetInteractable(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
            this.SetInteractable(null);
    }
    public abstract void Interact();
}
