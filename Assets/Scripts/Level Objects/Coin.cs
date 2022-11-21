using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Throwable
{
    [SerializeField]AudioClip coinSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy;
        if (enemy = collision.gameObject.GetComponent<Enemy>())
        {
            GameManager.Instance.PlaySound(coinSound);
            enemy.BecomeDistracted(5);
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<PlayerController>())
            Collect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
            Collect();
    }

}
