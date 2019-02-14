using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Background : MonoBehaviour {

    public Transform leftPoint;
    public Transform rightPoint;

    private TutorialManager tutorialScript;

    private void Update()
    {
        if (transform.position.x <= leftPoint.position.x)
            transform.position = rightPoint.position;
    }

}
