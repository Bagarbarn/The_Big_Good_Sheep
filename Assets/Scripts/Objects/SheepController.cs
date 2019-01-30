using System.Collections;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<BulletScript>().p_color == m_demandedColor)
            {
                Destroy(other);
                Destroy(gameObject);
            }
        }
    }


}


