using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public float p_minSpeed;
    public float p_maxSpeed;
    public float p_maxTimer;

    public float screenHeight;
    public float screenLow;

    private float m_score;
    public float p_customerPoints;
    public Text p_scoreText;

    //public float customerSpawnTime;


    public float startTime;
<<<<<<< Updated upstream


    public GameObject customerObject;
    public GameObject[] roadObstacles;

=======
    public GameObject customerObject;
>>>>>>> Stashed changes
    private float timeCounter;
    private float m_acceleration;
    private GameObject[] objectSpawners;

    //private float customerSpawnTimeCounter;

    [HideInInspector]
    public float m_currentSpeed;


	// Use this for initialization
	void Awake () {

        timeCounter = startTime;
        m_acceleration = (p_maxSpeed - p_minSpeed) / p_maxTimer;
        m_currentSpeed = p_minSpeed;
        //customerSpawnTimeCounter = customerSpawnTime;
<<<<<<< Updated upstream

        objectSpawners = GameObject.FindGameObjectsWithTag("ObstacleSpawner");

=======
        m_score = 0;
        p_scoreText.text = "Score: " + m_score;
>>>>>>> Stashed changes
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
            SpawnRandomObject();
        else
            timeCounter -= Time.deltaTime;
	}

    void SpawnRandomObject()
    {

        int sheep_or_object = Random.Range(0, 2);

        switch (sheep_or_object)
        {
            case 0: SpawnSheep(); break;
            case 1: SpawnRoadObstacle();  break;
            default: SpawnRandomObject(); break;
        }
    }

    void SpawnRoadObstacle()
    {

        //I am a dwarf and I'm digging a hole

        int object_choice = Random.Range(0, roadObstacles.Length);
        int spawner_choice = Random.Range(0, objectSpawners.Length);

        Instantiate(roadObstacles[object_choice], objectSpawners[spawner_choice].transform.position, Quaternion.identity);


    }

    void SpawnSheep()
    {
        float y_pos = Random.Range(screenLow, screenHeight);
        Vector2 spawnPos = new Vector2(11, y_pos);

        Instantiate(customerObject, spawnPos, Quaternion.identity);
        timeCounter = startTime;
    }

    public void AddPoint()
    {
        m_score += p_customerPoints;
        p_scoreText.text = "Score: " + m_score;
    }
}
