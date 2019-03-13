using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityScript : Obstacle {

    public float duration;

    private IEnumerator coroutine;

    public override void ObstacleEvent(GameObject playerObject)
    {
        base.ObstacleEvent(playerObject);
        PlayerController pc = playerObject.GetComponent<PlayerController>();
        coroutine = pc.Invincibility(duration);
        pc.StartCoroutine(coroutine);
    }

}
