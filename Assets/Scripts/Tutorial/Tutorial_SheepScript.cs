using System.Collections;
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

    private Animator m_animator;
    private BoxCollider2D m_collider;

    private void Awake()
    {
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        m_animator = gameObject.GetComponent<Animator>();
        m_collider = gameObject.GetComponent<BoxCollider2D>();

        canBeCollided = false;
        particleScript = GameObject.FindWithTag("GameController").GetComponent<ParticleManager>();
    }


    public void ObtainColor(string color)
    {
        m_demandedColor = color;
        if(demandSprite != null)
            demandSprite.color = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>().GetColor(m_demandedColor);

    }

    public void StopAnimation()
    {
        m_animator.speed = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            string color = other.GetComponent<BulletScript>().p_color;
            Color color_color = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>().GetColor(color);
            if (color == "Rainbow") color_color = Color.black;
            particleScript.SpawnParticleSystem(particleSystem, transform.position, color_color);

            if (other.gameObject.GetComponent<BulletScript>().p_color == m_demandedColor ||
                other.gameObject.GetComponent<BulletScript>().p_color == "Rainbow")
            {
                soundManager.PlayAudio(satisfiedBleat);
                FloatTextController.CreateFloatingText("10p", transform, true);
                FloatTextController.CreateFloatingText("2s", transform, true);

                m_animator.speed = 1.0f;
                m_animator.SetBool("isSatisfied", true);
                m_collider.enabled = false;

                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                Destroy(other.gameObject);
                Destroy(gameObject, 0.7f);

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
