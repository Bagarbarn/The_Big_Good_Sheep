﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MovingObjects {

    private string m_demandedColor;

    SpriteRenderer demandSprite;

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
<<<<<<< Updated upstream
                Destroy(other.gameObject);
=======
                gameManagerScript.AddPoint();
                Destroy(other);
>>>>>>> Stashed changes
                Destroy(gameObject);
            }
            else
            {
                Destroy(other.gameObject);
                //Something when wrong color hits sheeps
            }
        }
    }


}


