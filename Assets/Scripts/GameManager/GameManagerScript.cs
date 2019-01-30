using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public float p_minSpeed;
    public float p_maxSpeed;
    public float p_maxTimer;

    public float screenHeight;
    public float screenLow;


    //public float customerSpawnTime;


    public float startTime;

    public GameObject customerObject;

    private float timeCounter;

    private float m_acceleration;

    //private float customerSpawnTimeCounter;

    [HideInInspector]
    public float m_currentSpeed;


	// Use this for initialization
	void Awake () {

        timeCounter = startTime;
        m_acceleration = (p_maxSpeed - p_minSpeed) / p_maxTimer;
        m_currentSpeed = p_minSpeed;
        //customerSpawnTimeCounter = customerSpawnTime;

    }
	
	// Update is called once per frame
	void Update () {

        if (m_currentSpeed < p_maxSpeed)
            m_currentSpeed += m_acceleration * Time.deltaTime;

        if (timeCounter > 0)
            timeCounter -= Time.deltaTime;
        else
            Debug.Log("Game Ends");

        if (timeCounter <= 0)
            SpawnSheep();
        else
            timeCounter -= Time.deltaTime;
	}

    void SpawnSheep()
    {
        float y_pos = Random.Range(screenLow, screenHeight);
        Vector2 spawnPos = new Vector2(11, y_pos);

        Instantiate(customerObject, spawnPos, Quaternion.identity);
        timeCounter = startTime;
    }
}
