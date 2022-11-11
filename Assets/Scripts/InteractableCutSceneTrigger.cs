using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCutSceneTrigger : CutSceneTrigger
{
    [SerializeField] private Interactable IOTrigger;
    private void Update()
    {
        if (IOTrigger != null)
        {
            OnInteractableTrigger(IOTrigger.IOTrigger);
        }
    }

    private void OnInteractableTrigger(bool io)
    {
        if (io)
            director.PlayScene();
    }
}
