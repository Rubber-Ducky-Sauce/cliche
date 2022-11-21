using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip menu;
    public AudioClip level1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
    }

}
