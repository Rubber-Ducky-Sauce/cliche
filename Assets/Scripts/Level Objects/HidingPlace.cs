using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : Interactable
{
    PlayerController player;
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
        player.isHiding = true;
    }

    void ExitHiding()
    {
        if(player.isHiding && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            player.isHiding = false;
    }
}
