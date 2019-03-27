using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour {

    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public SoundScript soundScript;

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("SoundManager") != null)
        soundScript = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundScript>();
    }

    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }
    public void ClickSound()
    {
        if (soundScript != null)
            soundScript.PlayAudio(clickFx);
    }
}
