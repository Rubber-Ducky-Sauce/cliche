using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private float loadTime = 2f;

    [SerializeField] private string m_activeKey = null;
    [SerializeField] public Lock currentLock = null;
    [SerializeField] string lockLocked;

    [SerializeField] public Interactable m_currentInteractable = null;
    public Interactable currentInteractable { 
        get { return m_currentInteractable; } 
        set { m_currentInteractable = value; } }

    [SerializeField] public Collectable m_currentCollectable = null;
    public Collectable currentCollectable
    {
        get { return m_currentCollectable; }
        set { m_currentCollectable = value; }
    }

    [SerializeField] private bool m_gameIsActive = false;
    public bool gameIsActive
    {
        get { return m_gameIsActive; }
        private set { m_gameIsActive = value; }
    }

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetKey(string keyName)
    {
        activeKey = keyName;
    }

    //function useItem
        //if "oneUse" or depleted?
            //remove from state and itemBox
    public void DepleteItem()
    {
        Image itemBoxItem = GameObject.Find("Item").GetComponent<Image>();
        itemBoxItem.sprite = null;
        itemBoxItem.color = new Color(0,0,0,0);
        currentCollectable = null;
    }

    public void SetIsGameActive(bool io)
    {
        gameIsActive = io;
    }
}
