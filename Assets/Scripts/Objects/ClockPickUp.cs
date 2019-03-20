using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPickUp : Obstacle {


    public float p_timeToAdd;
    SoundScript soundManager;
    public AudioClip ClockSound;

    public override void ObstacleEvent(GameObject playerObject)
    {

        GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().AdjustTime(p_timeToAdd);
        FloatTextController.CreateFloatingText(p_timeToAdd.ToString() + "s", transform, true);
        base.ObstacleEvent(playerObject);
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        soundManager.PlayAudio(ClockSound);
    }

}
