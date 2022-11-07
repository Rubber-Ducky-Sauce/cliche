using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Collectable : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Image itemBoxItem;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }

    public virtual void Collect()
    {
        GameManager.Instance.currentCollectable = this;
        itemBoxItem = GameObject.Find("Item").GetComponent<Image>();
        itemBoxItem.sprite = spriteRenderer.sprite;
        itemBoxItem.color = spriteRenderer.color;
        gameObject.SetActive(false);
        
    }
}
