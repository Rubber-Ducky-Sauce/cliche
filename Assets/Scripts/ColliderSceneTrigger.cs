using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ColliderSceneTrigger : CutSceneTrigger
{

    private void Start()
    {
        base.SetDirector();
        CheckisTrigger();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        director.PlayScene();
    }

    private void CheckisTrigger()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (!collider.isTrigger) collider.isTrigger = true;
    }
}
