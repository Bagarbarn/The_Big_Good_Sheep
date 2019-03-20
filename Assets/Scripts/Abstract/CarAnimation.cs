using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimation : MonoBehaviour {

    public float rotateSpeed = 10.0f;

    public float speed_max = .2f;
    public float speed_addition = 0.1f;
    float speed_current;

    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject chassi;

    private Transform wheel1_trans;
    private Transform wheel2_trans;
    private Transform chassi_trans;

    private bool going_up;

    void Awake()
    {
        wheel1_trans = wheel1.GetComponent<Transform>();
        wheel2_trans = wheel2.GetComponent<Transform>();
        chassi_trans = chassi.GetComponent<Transform>();
        speed_current = speed_max;
        going_up = true;
    }

    void Update()
    {
        wheel1_trans.Rotate(Vector3.forward * -rotateSpeed);
        wheel2_trans.Rotate(Vector3.forward * -rotateSpeed);
        
        chassi_trans.Translate(Vector2.up * speed_current * Time.deltaTime);
        if (going_up == true)
        {
            speed_current -= speed_addition * Time.deltaTime;
            if (speed_current <= -speed_max)
            {
                speed_current = -speed_max;
                going_up = false;
            }
        }
        else if (going_up == false)
        {
            speed_current += speed_addition * Time.deltaTime;
            if (speed_current >= speed_max)
            {
                speed_current = speed_max;
                going_up = true;
            }
        }

    }
}
