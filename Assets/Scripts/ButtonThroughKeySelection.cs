using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonThroughKeySelection : MonoBehaviour
{

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(
                     this.gameObject);
    }

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(
                     this.gameObject);
    }

}
