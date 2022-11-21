using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public abstract class Collectable : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public AudioClip pickUpSound;
    Image itemBoxItem;

    [SerializeField] float dropForce;
    [SerializeField] float dropheight;
    [SerializeField] private Vector3 DropOffset;


    protected new Rigidbody2D rigidbody;
    protected PlayerController player;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Collect()
    {

        GameManager.Instance.PlaySound(pickUpSound);
        if (GameManager.Instance.currentCollectable != null)
        GameManager.Instance.currentCollectable.DropItem();
        GameManager.Instance.currentCollectable = this;
        itemBoxItem = GameObject.Find("Item").GetComponent<Image>();
        itemBoxItem.sprite = spriteRenderer.sprite;
        itemBoxItem.color = spriteRenderer.color;
        gameObject.SetActive(false);
    }

    public virtual void Use()
    {
        GameManager.Instance.DepleteItem();
    }

    public virtual void DropItem()
    {
        GetReferences();
        this.gameObject.SetActive(true);
        transform.position = player.transform.position + DropOffset;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;

        rigidbody.AddForce(new Vector2(player.isFacingLeft ? dropForce : -dropForce, dropheight), ForceMode2D.Impulse);
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
