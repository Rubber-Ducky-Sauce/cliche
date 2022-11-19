using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Throwable : Collectable
{
    [SerializeField] float throwForce;
    [SerializeField] float throwheight;
    [SerializeField] private Vector3 offset;

    public override void Use()
    {
        ThrowItem();
        base.Use();
    }

    public virtual void ThrowItem()
    {
        GetReferences();
        this.gameObject.SetActive(true);
        transform.position = player.transform.position + offset;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        
        rigidbody.AddForce(new Vector2(player.isFacingLeft? -throwForce: throwForce,throwheight),ForceMode2D.Impulse);
        GetComponent<PolygonCollider2D>().isTrigger = false;
    }

    private void GetReferences()
    {
        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
}