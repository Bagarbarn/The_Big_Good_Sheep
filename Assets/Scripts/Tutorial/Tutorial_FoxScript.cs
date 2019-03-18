using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_FoxScript : MonoBehaviour {

    [HideInInspector]
    public string m_demandedColor;

    private SpriteRenderer demandSprite;
    public AudioClip evilLaughter;
    private SoundScript soundManager;
    [HideInInspector]
    public bool canBeCollided;

    private void Awake()
    {
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        canBeCollided = false;
    }


    public void ObtainColor(string color)
    {
        m_demandedColor = color;
        if (demandSprite != null)
            demandSprite.color = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>().GetColor(m_demandedColor);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && !canBeCollided)
        {
            soundManager.PlayAudio(evilLaughter);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player" && canBeCollided)
        {
            Destroy(this.gameObject);
        }
    }
}
