using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MovingObjects {

    public float startPos, endPos;

    public GameObject other_ground;

    public float offset;

    public override void Start()
    {
        base.Start();
        spriteRenderer.sortingOrder = -1000;
        offset = other_ground.GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;
        endPos = -offset;
    }

    // Update is called once per frame
    override public void Update () {
        base.Update();

        if (transform.position.x < endPos)
        {
            transform.position = new Vector3(other_ground.transform.position.x + offset, transform.position.y, 0);
        }

	}
}
