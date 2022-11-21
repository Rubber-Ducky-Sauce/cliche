using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : Interactable
{
    PlayerController player;
    AudioSource audioSource;
    [SerializeField]AudioClip clip;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        ExitHiding();
    }
    public override void Interact()
    {
        player.isHiding = true;
        audioSource.PlayOneShot(clip);

    }

    void ExitHiding()
    {
        if(player.isHiding && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            player.isHiding = false;
            audioSource.PlayOneShot(clip);
        }
    }
}
