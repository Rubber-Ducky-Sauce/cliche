using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
    [SerializeField] private string keyName;
    // Start is called before the first frame update
    public override  void Collect()
    {
        base.Collect();
        GameManager.Instance.SetKey(keyName);
    }

    public override void Use()
    {
        Debug.Log("need to make use case for key");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
            Collect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
            Collect();
    }
}
