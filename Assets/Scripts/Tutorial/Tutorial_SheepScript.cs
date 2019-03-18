﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_SheepScript : MonoBehaviour {

    [HideInInspector]
    public string m_demandedColor;

    private SpriteRenderer demandSprite;
    public AudioClip satisfiedBleat;
    private SoundScript soundManager;
    [HideInInspector]
    public bool canBeCollided;

    private ParticleManager particleScript;

    public GameObject particleSystem;

    private void Awake()
    {
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        canBeCollided = false;
        particleScript = GameObject.FindWithTag("GameController").GetComponent<ParticleManager>();
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
            string color = other.GetComponent<BulletScript>().p_color;
            Color color_color = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>().GetColor(color);
            particleScript.SpawnParticleSystem(particleSystem, transform.position, color_color);

            if (other.gameObject.GetComponent<BulletScript>().p_color == m_demandedColor ||
                other.gameObject.GetComponent<BulletScript>().p_color == "Rainbow")
            {
                soundManager.PlayAudio(satisfiedBleat);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            } else
            {
                Destroy(other.gameObject);
            }
        } else if (other.tag == "Player" && canBeCollided)
        {
            Destroy(this.gameObject);
        }
    }
}
