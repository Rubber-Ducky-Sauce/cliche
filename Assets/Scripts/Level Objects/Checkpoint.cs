using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] AudioClip checkpointAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            GameManager.Instance.PlaySound(checkpointAudio);
            GameManager.Instance.currentCheckpoint = this.name;
            gameObject.SetActive(false);
        }
    }
}
