using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPickUp : Obstacle {


    public float p_timeToAdd;

    public override void ObstacleEvent(GameObject playerObject)
    {

        GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().AdjustTime(p_timeToAdd);

        base.ObstacleEvent(playerObject);
    }

}
