using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private float loadTime = 2f;

    [SerializeField] private string m_activeKey = null;
    [SerializeField] public Lock currentLock = null;
    [SerializeField] string lockLocked;

    public string activeKey { get { return m_activeKey; } private set { m_activeKey = value; } }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (currentLock != null)
        {
            lockLocked = currentLock ? "Locked" : "unlocked";
        }
        else lockLocked = "no lock";
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetKey(string keyName)
    {
        activeKey = keyName;
    }
}
