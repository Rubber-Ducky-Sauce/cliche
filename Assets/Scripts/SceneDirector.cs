using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    [SerializeField] bool readyToLoad = false;
    [SerializeField] float loadTime = 2f;
    [SerializeField] string nextScene;
    [SerializeField] private bool isGameActive;
    [SerializeField] bool sceneActive = true;
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
    private void Start()
    {
        DeactivateGame();
    }
    private void Update()
    {
        GetIsGameActive();
        ReadyNextScene();
        ActivateGame();
    }

    private void ReadyNextScene()
    {
        if (readyToLoad) 
            StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadTime);
        GameManager.Instance.LoadScene(nextScene);
    }

    private void GetIsGameActive()
    {
        isGameActive = GameManager.Instance.gameIsActive;
    }

    private void ActivateGame()
    {
        if (!sceneActive)
        {
            GameManager.Instance.SetIsGameActive(true);
        }
    }
    private void DeactivateGame()
    {
      GameManager.Instance.SetIsGameActive(false);
    }
}
