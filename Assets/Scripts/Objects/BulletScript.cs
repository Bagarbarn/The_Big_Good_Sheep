using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


    public string p_color;

    private float m_speed;

    private void Start()
    {
        m_speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().bullet_speed;
        Destroy(this.gameObject, 20 / m_speed);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * m_speed * Time.deltaTime);
    }


}
