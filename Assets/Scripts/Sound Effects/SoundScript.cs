using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    public AudioSource[] audioSources;

    private int GetFreeSourceID()
    {
        for (int i = 0; i < audioSources.Length; i++){
            if (!audioSources[i].isPlaying)
                return i;}
        return -1;
    }

    public void PlayAudio(AudioClip clip)
    {
        AudioSource freeSource;
        int index = GetFreeSourceID();
        if (index != -1)
        {
            freeSource = audioSources[index];
            freeSource.clip = clip;
            freeSource.Play();
        }
        else Debug.Log("No free sources available.");
    }

}
