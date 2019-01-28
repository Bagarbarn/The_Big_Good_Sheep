using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public float p_minSpeed;
    public float p_maxSpeed;
    public float p_maxTimer;


    public float startTime;

    private float timeCounter;

    private float m_acceleration;

    [HideInInspector]
    public float m_currentSpeed;


	// Use this for initialization
	void Awake () {

        timeCounter = startTime;
        m_acceleration = (p_maxSpeed - p_minSpeed) / p_maxTimer;
        m_currentSpeed = p_minSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        if (m_currentSpeed < p_maxSpeed)
        {
            m_currentSpeed += m_acceleration;
        }

        if (timeCounter > 0)
            timeCounter -= Time.deltaTime;
        else
            Debug.Log("Game Ends");
	}
}
