using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocksCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI InfoText;
    bool keyAcquired = false;
    private void Update()
    {
        GrabKey();
        TryDoor();
    }

    void GrabKey()
    {
        if(!keyAcquired && GameManager.Instance.activeKey != "")
        {
            keyAcquired = true;
            InfoText.text = "KEY ACQUIRED";
        }
    }

    void TryDoor()
    {
        if(Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentInteractable.GetType() == typeof(Lock) )
        {
           _=keyAcquired? InfoText.text = "Door Unlocked!!": InfoText.text = "requires key";
        }
    }
}
