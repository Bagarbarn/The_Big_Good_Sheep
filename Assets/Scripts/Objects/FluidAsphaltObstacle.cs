using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidAsphaltObstacle : Obstacle {

    public int p_effectTime;
    public float p_slowPercentage;

    private IEnumerator coroutine;

    public override void Start()
    {
        base.Start();
        spriteRenderer.sortingOrder = -900;
    }

    public override void ObstacleEvent(GameObject playerObject)
    {
        playerObject.GetComponent<PlayerController>().p_slowPercentage = this.p_slowPercentage;
        coroutine = playerObject.GetComponent<PlayerController>().SetSlowed(p_effectTime);
        playerObject.GetComponent<PlayerController>().StartCoroutine(coroutine);

    }
}

