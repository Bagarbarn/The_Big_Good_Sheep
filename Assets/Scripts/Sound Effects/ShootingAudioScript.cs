using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAudioScript : MonoBehaviour {
    public AudioClip ShootingClip;

    public AudioSource ShootingSource;

	// Use this for initialization
	void Start () {
        ShootingSource.clip = ShootingClip;


	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            ShootingSource.Play();
	}
}
