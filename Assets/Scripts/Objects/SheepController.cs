using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MovingObjects {
    
    private string m_demandedColor;

    SpriteRenderer demandSprite;

    public int p_scoreValue;
    public int p_scoreFailure;
    public int p_timeWhenHit;

    override public void Start()
    {
        base.Start();
        m_demandedColor = gameManagerScript.gameObject.GetComponent<ColorManager>().GetRandomColor();
        Debug.Log(m_demandedColor);
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        demandSprite.color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(m_demandedColor);
        Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {

            if (other.gameObject.GetComponent<BulletScript>().p_color == m_demandedColor)
            {
                gameManagerScript.AddScore(p_scoreValue);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(other.gameObject);
                gameManagerScript.AddScore(-p_scoreFailure);
                //Something when wrong color hits sheeps
            }
        } else if (other.gameObject.tag == "Player")
        {
            gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(-p_timeWhenHit);
            Debug.Log("Sheep Hit");
            Destroy(this.gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(-p_timeWhenHit);
    //        Debug.Log("Sheep Hit");
    //    }
    //}


}


