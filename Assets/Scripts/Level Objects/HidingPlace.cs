using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : Interactable
{
    PlayerController player;
    [SerializeField]AudioClip clip;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        ExitHiding();
    }
    public override void Interact()
    {
        if (!player.isHiding)
        {
            StartCoroutine(QuickDisablePlayer());
            player.isHiding = true;
            GameManager.Instance.PlaySound(clip);
        }

    }

    void ExitHiding()
    {
        if(player.isHiding && player.playerActive && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            player.isHiding = false;
            GameManager.Instance.PlaySound(clip);
        }
    }

    IEnumerator QuickDisablePlayer()
    {
        player.playerActive = false;
        yield return new WaitForSeconds(.5f);
        player.playerActive = true;
    }
}
