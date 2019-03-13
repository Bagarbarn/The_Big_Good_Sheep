using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Background : MonoBehaviour {

    //public Transform leftPoint;
    //public Transform rightPoint;

    public GameObject other_ground;

    public float offset;

    private float endPos;

    private void Start()
    {
        offset = other_ground.GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;
        endPos = -offset;
    }

    private TutorialManager tutorialScript;

    private void Update()
    {
        if (transform.position.x <= endPos)
            transform.position = new Vector2(other_ground.transform.position.x + offset, 0);
    }

}
