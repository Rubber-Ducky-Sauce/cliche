using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    [SerializeField] private bool locked = true;
    [SerializeField] string keyName;

    public override void Interact()
    {
        if (locked &&
            GameManager.Instance.activeKey == keyName)
        {
            locked = false;
            Debug.Log("door unlocked");
            GameManager.Instance.DepleteItem();
        }
        if (!locked)
        {
            Debug.Log("not locked...");
        }
        else Debug.Log("Need Specific Key");
    }

    public void OtherFunct()
    {
        if (!locked)
        {
            Debug.Log("okay, good");
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameManager.Instance.currentLock = this;
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    GameManager.Instance.currentLock = null;
    //}

}
