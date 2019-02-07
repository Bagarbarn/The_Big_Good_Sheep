using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip shotAudio;
    public AudioClip splatAudio;


    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    public void PlayShotAudio()
    {
        audioSource.clip = shotAudio;
        audioSource.Play();
    }

    public void PlaySplatAudio()
    {
        audioSource.clip = splatAudio;
        audioSource.Play();
    }

}
