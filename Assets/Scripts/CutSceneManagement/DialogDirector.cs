using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDirector : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog.Line currentLine;

    private void Start()
    {
        foreach(Dialog.Line line in dialog.ScriptDialog)
        {
            Debug.Log($"{dialog.actors[line.actor]}: {line.line}");
        }
    }
}
