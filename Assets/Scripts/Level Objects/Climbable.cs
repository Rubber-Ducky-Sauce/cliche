using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : Interactable
{
    [SerializeField] float climbPoint;

    public override void TriggerEnter(GameObject player)
    {
        base.TriggerEnter(player);
        iPlayer.DeactivateInteractNotice();
    }
}
