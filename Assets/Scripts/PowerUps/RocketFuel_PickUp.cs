using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFuel_PickUp : Obstacle {

	public int p_effectTime;
    public float p_boostPercentage;

    private IEnumerator coroutine;

    public override void ObstacleEvent(GameObject playerObject)
    {
        base.ObstacleEvent(playerObject);
        playerObject.GetComponent<PlayerController>().p_boostPercentage = this.p_boostPercentage;
        coroutine = playerObject.GetComponent<PlayerController>().SetBoost(p_effectTime);
        playerObject.GetComponent<PlayerController>().StartCoroutine(coroutine);
    }
}
