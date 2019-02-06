using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxCustomerController : MovingObjects {

    private string m_demandedColor;

    SpriteRenderer demandSprite;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
        m_demandedColor = gameManagerScript.gameObject.GetComponent<ColorManager>().GetRandomColor();
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        demandSprite.color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(m_demandedColor);
        Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
