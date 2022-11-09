using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    [SerializeField]bool readyToLoad = false;
    [SerializeField] float loadTime = 2f;
    [SerializeField] string nextScene;

    private void Update()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (readyToLoad) SceneManager.LoadScene(nextScene);
    }
}
