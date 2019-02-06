using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour {

    public GameObject introObject;

    public float p_displayTime;
    public float p_blinkTime;
    public float p_blinkInterval;

    IEnumerator coroutine;

	void Start () {


        coroutine = SetIntro(introObject);
        StartCoroutine(coroutine);

	}

    IEnumerator SetIntro(GameObject intro)
    {
        bool active = true;
        float timePassed = 0;
        intro.SetActive(active);
        while (p_displayTime > 0)
        {
            p_displayTime -= Time.deltaTime;
            yield return null;
        }
        while (p_blinkTime > 0)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= p_blinkInterval)
            {
                if (active == true)
                    active = false;
                else
                    active = true;

                intro.SetActive(active);

                timePassed = 0;
            }
            p_blinkTime -= Time.deltaTime;
            yield return null;
        }

        intro.SetActive(false);
    }

}
