using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MovingObjects {
    
    private string m_demandedColor;

    SpriteRenderer demandSprite;
    private SoundScript soundManager;
    public AudioClip satisfiedBleat;
    public AudioClip satisfiedBleat2;

    public float p_timeValue;
    public int p_scoreValue;
    public int p_scoreFailure;
    public int p_timeWhenHit;

    public Vector2 speedValues;
    public Vector2 modifier_SpeedModifier_secondsToMax;

    private float timeSpeedModifier;
    private float time;

    private ScreenShake ss;

    override public void Start()
    {
        base.Start();
        if (Time.timeSinceLevelLoad < modifier_SpeedModifier_secondsToMax.y)
            time = Time.timeSinceLevelLoad;
        else
            time = modifier_SpeedModifier_secondsToMax.y;

        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();

        //Gets modifier for speed increase
        timeSpeedModifier = modifier_SpeedModifier_secondsToMax.x * (time / modifier_SpeedModifier_secondsToMax.y);
        p_speed = Random.Range(speedValues.x, speedValues.y) * timeSpeedModifier;

        //Gets demanded color
        bool multicolor = gameManagerScript.gameObject.GetComponent<SpawnManager>().GetColorMode();
        m_demandedColor = gameManagerScript.gameObject.GetComponent<ColorManager>().GetRandomColor(multicolor);
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        demandSprite.color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(m_demandedColor);

        //Get Camera script

        ss = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();

        //Destroys sheep outside screen
        Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
    }

    AudioClip RandomizeBleat()
    {
        int rand = Random.Range(1, 5);
        if (rand >= 3)
            return satisfiedBleat;
        else return satisfiedBleat2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<BulletScript>().p_color == m_demandedColor || other.gameObject.GetComponent<BulletScript>().p_color == "Rainbow")
            {
                gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(p_timeValue);
                gameManagerScript.AddScore(p_scoreValue);
                soundManager.PlayAudio(RandomizeBleat());

                FloatTextController.CreateFloatingText((gameManagerScript.m_multiplier*p_scoreValue).ToString() + "p", transform, true);
                FloatTextController.CreateFloatingText(p_timeValue.ToString() + "s", transform, true);

                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(other.gameObject);
                gameManagerScript.AddScore(-p_scoreFailure);
                FloatTextController.CreateFloatingText("-"+p_scoreFailure.ToString()+"p", transform, false);
                //Something when wrong color hits sheeps
            }
        } else if (other.gameObject.tag == "Player")
        {
            gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(-p_timeWhenHit);
            FloatTextController.CreateFloatingText("-"+p_timeWhenHit.ToString()+"s", transform, false);
            ss.ShakeScreen();
            //Debug.Log("Sheep Hit");
            Destroy(this.gameObject);
        }
    }




}


