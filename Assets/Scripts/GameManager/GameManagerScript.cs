using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public bool t_gameEnd;

    public float p_minSpeed;
    public float p_maxSpeed;
    public float p_maxTimer;

    public float screenHeight;
    public float screenLow;

    public int fauxSpawnPercent;
    public float customerSpawnTime;
    public float obstacleSpawnTime;

    public GameObject customerObject;
    public GameObject fauxCustomerObject;

    public Vector2 pickUpsVsRoadObstacles;
    public GameObject[] roadObstacles;
    public GameObject[] pickUpObjects;
    public float[] roadObstaclesSpawnChance;
    public float[] pickUpSpawnChance;
   

    public Text p_scoreText;

    //public int p_customerPoints;
    private int m_score;

    
    public float customerTimeCounter;
    public float obstacleTimeCounter;

    private float m_acceleration;
    private GameObject[] objectSpawners;

    //private float customerSpawnTimeCounter;

    [HideInInspector]
    public float m_currentSpeed;


	// Use this for initialization
	void Awake () {

        customerTimeCounter = customerSpawnTime;
        obstacleTimeCounter = obstacleSpawnTime;
        m_acceleration = (p_maxSpeed - p_minSpeed) / p_maxTimer;
        m_currentSpeed = p_minSpeed;

        objectSpawners = GameObject.FindGameObjectsWithTag("ObstacleSpawner");
        p_scoreText.text = "Score: " + m_score;

    }
	
	// Update is called once per frame
	void Update () {

        if (m_currentSpeed < p_maxSpeed)
            m_currentSpeed += m_acceleration * Time.deltaTime;

        if (customerTimeCounter <= 0)
            SpawnSheep();
        else
            customerTimeCounter -= Time.deltaTime;

        if (obstacleTimeCounter <= 0)
            SpawnRoadObstacle();
        else
            obstacleTimeCounter -= Time.deltaTime;


        if (Mathf.Abs(obstacleTimeCounter - customerTimeCounter) < 0.25f)
            obstacleTimeCounter += 0.5f;

        //FOR TESTING
        //if (m_score > 10)
        //{
        //    GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolderScript>().p_endScore = m_score;
        //    SceneManager.LoadScene("ScoreBoard");
        //}
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
        //timeCounter = startTime;
    }

    void SpawnRoadObstacle()
    {
        float random = Random.Range(1, 101);

        if (random <= pickUpsVsRoadObstacles.x)
        {
            float pickUp_choice = Random.Range(1, 101);
            int numberToSpawn = 0;
            for (int i = 0; i < pickUpSpawnChance.Length; i++)
            {
                if (pickUp_choice <= pickUpSpawnChance[i])
                {
                    numberToSpawn = i;
                    break;
                }
                else
                    pickUp_choice -= pickUpSpawnChance[i];
            }



            int spawner_choice = Random.Range(0, objectSpawners.Length);

            Instantiate(pickUpObjects[numberToSpawn], objectSpawners[spawner_choice].transform.position, Quaternion.identity);
        }
        else
        {

            float object_choice = Random.Range(1, 101);
            int numberToSpawn = 0;
            for (int i = 0; i < roadObstaclesSpawnChance.Length; i++)
            {
                if (object_choice <= roadObstaclesSpawnChance[i])
                {
                    numberToSpawn = i;
                    break;
                }
                else
                    object_choice -= roadObstaclesSpawnChance[i];
            }
            int spawner_choice = Random.Range(0, objectSpawners.Length);
            Instantiate(roadObstacles[numberToSpawn], objectSpawners[spawner_choice].transform.position, Quaternion.identity);
        }
        obstacleTimeCounter = obstacleSpawnTime;
    }

    void SpawnSheep()
    {

        GameObject gameObjectToSpawn;
        int rand = Random.Range(1, 101);
        if (rand <= fauxSpawnPercent)
            gameObjectToSpawn = fauxCustomerObject;
        else
            gameObjectToSpawn = customerObject;


        float y_pos = Random.Range(screenLow, screenHeight);
        Vector2 spawnPos = new Vector2(11, y_pos);

        Instantiate(gameObjectToSpawn, spawnPos, Quaternion.identity);
        customerTimeCounter = customerSpawnTime;
    }

    public void AddScore(int scoreToAdd)
    {
        m_score += scoreToAdd;
        p_scoreText.text = "Score: " + m_score;
    }

    public void EndGame()
    {
        GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolderScript>().p_endScore = m_score;
        Debug.Log("Times up! \nWait... Am I supposed to do something here?");
        if (t_gameEnd)
            SceneManager.LoadScene("ScoreBoard");
    }
}
