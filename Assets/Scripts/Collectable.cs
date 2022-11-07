using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }

    public virtual void Collect()
    {
        GameManager.Instance.currentCollectable = this;
        gameObject.SetActive(false);
    }
}
