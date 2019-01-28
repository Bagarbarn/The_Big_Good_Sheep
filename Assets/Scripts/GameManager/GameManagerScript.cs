using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public float p_minSpeed;
    public float p_maxSpeed;

    public float p_maxTimer;


    private float m_acceleration;

    [HideInInspector]
    public float m_currentSpeed;


	// Use this for initialization
	void Awake () {

        m_acceleration = (p_maxSpeed - p_minSpeed) / p_maxTimer;
        m_currentSpeed = p_minSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        if (m_currentSpeed < p_maxSpeed)
        {
            m_currentSpeed += m_acceleration;
        }
	}
}
