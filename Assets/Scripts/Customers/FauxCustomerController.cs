using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxCustomerController : MovingObjects {

    public float p_decreaseTime;
    public int p_increasePoints;

    private string m_demandedColor;

    SpriteRenderer demandSprite;
    SoundScript soundManager;
    public AudioClip evilLaughter;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
        m_demandedColor = gameManagerScript.gameObject.GetComponent<ColorManager>().GetRandomColor();
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        demandSprite.color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(m_demandedColor);
        Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(-p_decreaseTime);
            FloatTextController.CreateFloatingText("-"+p_decreaseTime.ToString()+"s", transform, false);
            //Cool smoke bomb animation with evil laughter
            soundManager.PlayAudio(evilLaughter);
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        } else if (other.tag == "Player")
        {
            //They can die
            gameManagerScript.AddScore(p_increasePoints);
            FloatTextController.CreateFloatingText(p_increasePoints.ToString()+"p", transform, true);
            Destroy(this.gameObject);
        }
    }
}
