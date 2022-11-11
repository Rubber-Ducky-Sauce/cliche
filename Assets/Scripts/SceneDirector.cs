using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class SceneDirector : MonoBehaviour
{
    [SerializeField] bool readyToLoad = false;
    [SerializeField] float loadTime = 2f;
    [SerializeField] string nextScene;
    [SerializeField] private bool isGameActive;
    [SerializeField] bool sceneStarted = false;
    [SerializeField] bool sceneActive = false;

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
        if (GetComponent<PlayableDirector>().playOnAwake)
        {
            PlayScene();
        }
    }
    private void Update()
    {
        GetIsGameActive();
        ReactivateGame();
        ReadyNextScene();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            PlayScene();
    }


    private void ReadyNextScene()
    {
        if (readyToLoad)
        {
            readyToLoad = false; 
            StartCoroutine(LoadNextScene());
        }
            
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

    private void ReactivateGame()
    {
        if (sceneStarted && !sceneActive)
        {
            sceneStarted = false;
            GameManager.Instance.SetIsGameActive(true);
        }
    }
    private void DeactivateGame()
    {
      GameManager.Instance.SetIsGameActive(false);
    }

    public void PlayScene()
    {
        PlayableDirector director = GetComponent<PlayableDirector>();
        sceneStarted = true;
        sceneActive = true;
        DeactivateGame();
        director.Play();
    }
}
