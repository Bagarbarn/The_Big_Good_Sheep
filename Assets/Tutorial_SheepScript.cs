﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_SheepScript : MonoBehaviour {

    [HideInInspector]
    public string m_demandedColor;

    private SpriteRenderer demandSprite;

    private void Awake()
    {
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }


    public void ObtainColor(string color)
    {
        m_demandedColor = color;
        if(demandSprite != null)
            demandSprite.color = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>().GetColor(m_demandedColor);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<BulletScript>().p_color == m_demandedColor ||
                other.gameObject.GetComponent<BulletScript>().p_color == "Rainbow")
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            } else
            {
                Destroy(other.gameObject);
            }
        } else if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
