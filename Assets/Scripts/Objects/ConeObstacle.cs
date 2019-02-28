using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeObstacle : Obstacle {

    public float p_timeToSubstract;

    public override void ObstacleEvent(GameObject playerObject)
    {

        GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().AdjustTime(-p_timeToSubstract);
        FloatTextController.CreateFloatingText(p_timeToSubstract.ToString() + "s", transform, false);
        base.ObstacleEvent(playerObject);

    }
}
