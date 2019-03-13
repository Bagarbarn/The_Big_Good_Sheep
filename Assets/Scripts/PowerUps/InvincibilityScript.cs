using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityScript : Obstacle {

    public float duration;
    SoundScript soundManager;
    public AudioClip InvincibilitySound;
    private IEnumerator coroutine;

    public override void ObstacleEvent(GameObject playerObject)
    {
        base.ObstacleEvent(playerObject);
        PlayerController pc = playerObject.GetComponent<PlayerController>();
        coroutine = pc.Invincibility(duration);
        pc.StartCoroutine(coroutine);
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        soundManager.PlayAudio(InvincibilitySound);
    }

}
