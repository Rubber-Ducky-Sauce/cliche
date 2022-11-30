using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected bool isSceneTrigger;
    public bool IOTrigger = false;
    protected PlayerController iPlayer;
    public virtual void SetInteractable(Interactable interactable)
    {
        GameManager.Instance.currentInteractable = interactable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit(collision.gameObject);
    }

    public virtual void TriggerEnter(GameObject player)
    {
        if (player.GetComponent<PlayerController>())
        {
            iPlayer = player.GetComponent<PlayerController>();
            iPlayer.ActivateInteractNotice();
            this.SetInteractable(this);
        }
    }

    public virtual void TriggerExit(GameObject player)
    {
        if (player.GetComponent<PlayerController>())
        {
            iPlayer.DeactivateInteractNotice();
            this.SetInteractable(null);
        }
    }

    public virtual void Interact()
    {
        iPlayer.DeactivateInteractNotice();
    }
}
