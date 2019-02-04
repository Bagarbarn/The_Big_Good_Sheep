using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockObstacle : Obstacle {



    public override void ObstacleEvent(GameObject playerObject)
    {

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>().EndGame();

        base.ObstacleEvent(playerObject);
    }


}
