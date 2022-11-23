using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    [SerializeField] private bool locked = true;
    [SerializeField] string keyName;
    [SerializeField] AudioClip unlockClip;
    [SerializeField] AudioClip failClip;
    [SerializeField] GameObject removalble;

    public override void Interact()
    {
        if (locked &&
            GameManager.Instance.activeKey == keyName)
        {
            locked = false;
            Debug.Log("door unlocked");
            GameManager.Instance.DepleteItem();
            GameManager.Instance.SetKey("");
            GameManager.Instance.PlaySound(unlockClip);
            if(isSceneTrigger) IOTrigger = !locked;
            if(removalble != null) removalble.SetActive(false);
        }
        if (!locked)
        {
            Debug.Log("not locked...");
            GameManager.Instance.PlaySound(failClip);
        }
        else
        {
            Debug.Log("Need Specific Key");
            GameManager.Instance.PlaySound(failClip);
        }
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
