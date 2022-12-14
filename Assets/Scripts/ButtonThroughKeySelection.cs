using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonThroughKeySelection : MonoBehaviour
{
    Button button;
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(
                     this.gameObject);
        button = GetComponent<Button>();

        button.onClick.AddListener(() => GameManager.Instance.StartGame());
    }

    
    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(
                     this.gameObject);
    }

    
}
