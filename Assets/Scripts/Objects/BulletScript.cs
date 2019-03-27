using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public string p_color;

    private float m_speed;
    private SpriteRenderer spriteRenderer;

    //private SoundScript soundScript;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f) + 50;
            //400;
        m_speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().bullet_speed;
        Destroy(this.gameObject, 15 / m_speed);
        //soundScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundScript>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * m_speed * Time.deltaTime);
    }

}
