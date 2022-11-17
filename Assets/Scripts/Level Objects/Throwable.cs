using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Throwable : Collectable
{
    [SerializeField] float throwDistance;

    public override void Use()
    {
        ThrowItem();
        base.Use();
    }
    public virtual void ThrowItem()
    {
        this.gameObject.SetActive(true);
    }
}
