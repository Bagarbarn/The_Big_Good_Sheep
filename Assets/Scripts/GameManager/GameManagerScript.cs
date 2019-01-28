using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public float p_minSpeed;
    public float p_maxSpeed;

    public float p_maxTimer;


    private float m_speedIncrease;
    private float m_currentSpeed;


	// Use this for initialization
	void Start () {

        m_speedIncrease = (p_maxSpeed - p_minSpeed) / p_maxTimer;

	}
	
	// Update is called once per frame
	void Update () {

	}
    
}
