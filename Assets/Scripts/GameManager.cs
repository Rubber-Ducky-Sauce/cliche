using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private float loadTime = 2f;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
