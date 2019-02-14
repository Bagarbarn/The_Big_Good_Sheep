using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MovingObjects {

    public float startPos, endPos;

    public override void Start()
    {
        base.Start();
        spriteRenderer.sortingOrder = -1000;
    }

    // Update is called once per frame
    override public void Update () {
        base.Update();

        if (transform.position.x < endPos)
        {
            transform.position = new Vector3(startPos, transform.position.y, 0);
        }

	}
}
