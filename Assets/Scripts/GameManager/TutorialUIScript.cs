using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIScript : MonoBehaviour {

    //Game Objects
    public GameObject moveSprites;
    public GameObject shootSprite;

    //Text
    public Text infoText;
    public Text scoreText;
    public Text timeText;

    //Images
    public Image iceCreamRed;
    public Image iceCreamBlue;
    public Image iceCreamYellow;

    public Image keyRed;
    public Image keyBlue;
    public Image keyYellow;
    

    protected GameObject blinkObject;

    protected float blinkTime;
    public float preBlinkTime;
    public float blinkInterval;


    public void ChangeActive(GameObject p_object, bool p_active)
    {
        p_object.SetActive(p_active);
    }
    public void ChangeActive(Text p_object, bool p_active)
    {
        p_object.gameObject.SetActive(p_active);
    }
    public void ChangeActive(Image p_object, bool p_active)
    {
        p_object.gameObject.SetActive(p_active);
    }


    public void BlinkObject(GameObject p_blinkObject, float p_blinkTime)
    {
        blinkObject = p_blinkObject;
        blinkTime = p_blinkTime;
        StartCoroutine("blinkObjRoutine");
    }
    public void BlinkObject(Text p_blinkObject, float p_blinkTime)
    {
        blinkObject = p_blinkObject.gameObject;
        blinkTime = p_blinkTime;
        StartCoroutine("blinkObjRoutine");
    }
    public void BlinkObject(Image p_blinkObject, float p_blinkTime)
    {
        blinkObject = p_blinkObject.gameObject;
        blinkTime = p_blinkTime;
        StartCoroutine("blinkObjRoutine");
    }

    IEnumerator blinkObjRoutine()
    {
        bool active = true;
        float timePassed = 0;
        blinkObject.SetActive(active);

        while (preBlinkTime > 0)
        {
            preBlinkTime -= Time.deltaTime;
            yield return null;
        }
        while (blinkTime > 0)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= blinkInterval)
            {
                if (active == true)
                    active = false;
                else
                    active = true;

                blinkObject.SetActive(active);
                timePassed = 0;
            }
            blinkTime -= Time.deltaTime;
            yield return null;
        }
        blinkObject.SetActive(true);
    }
}
