using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdrivePickUp : Obstacle {

    public float overdriveTime;
    public float overdriveShotSpeed;
    SoundScript soundManager;
    public AudioClip OverDriveSound;

    private IEnumerator coroutine;

    public override void ObstacleEvent(GameObject playerObject)
    {
        PlayerController pc = playerObject.GetComponent<PlayerController>();
        coroutine = pc.Overdrive(overdriveTime);
        pc.p_overdriveShotInterval = overdriveShotSpeed;
        pc.StartCoroutine(coroutine);
        base.ObstacleEvent(playerObject);
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        soundManager.PlayAudio(OverDriveSound);
    }



}
