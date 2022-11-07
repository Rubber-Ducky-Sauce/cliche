using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void SetInteractable(Interactable interactable)
    {
        GameManager.Instance.currentInteractable = interactable;
    }

    public abstract void Interact();
}
