using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


    public string p_color;

    private float m_speed;

    private SoundScript soundScript;

    private void Start()
    {
        m_speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().bullet_speed;
        Destroy(this.gameObject, 20 / m_speed);
        soundScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundScript>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * m_speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (transform.position.x < 10.5)
            soundScript.PlaySplatAudio();
    }

}
