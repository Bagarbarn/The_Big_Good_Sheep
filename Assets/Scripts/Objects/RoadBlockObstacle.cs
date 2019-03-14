using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockObstacle : Obstacle {

    SoundScript soundManager;
    public AudioClip RoadBlockSound;

    public override void ObstacleEvent(GameObject playerObject)
    {

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>().EndGame();

        base.ObstacleEvent(playerObject);
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        soundManager.PlayAudio(RoadBlockSound);
    }


}
