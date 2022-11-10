using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    [SerializeField]bool readyToLoad = false;
    [SerializeField] float loadTime = 2f;
    [SerializeField] string nextScene;
    //[SerializeField] List<string> actors;

    //[Serializable]
    //struct Dialog
    //{
    //    public enum actor { actors };
    //    public string dialog;


    //    public class Dialog(enum actor, string dialog)
    //    {
    //        this.actor = actor;
    //        this.dialog = dialog;
    //    }
    //}

    //[SerializeField] private List<Dialog> ScriptDialog;

    private void Update()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (readyToLoad) SceneManager.LoadScene(nextScene);
    }
}
