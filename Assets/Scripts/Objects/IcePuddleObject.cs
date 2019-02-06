using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePuddleObject : Obstacle {

    public int p_stunTime;

    private IEnumerator coroutine;

    public override void ObstacleEvent(GameObject playerObject)
    {
        Debug.Log("IceHit");
        coroutine = playerObject.GetComponent<PlayerController>().SetStunned(p_stunTime);
        playerObject.GetComponent<PlayerController>().StartCoroutine(coroutine);
    }
}
