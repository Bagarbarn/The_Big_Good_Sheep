using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    
    public GameObject customer_object;
    public GameObject fauxCustomer_object;
    public GameObject[] pickUp_objects;
    public int[] pickUp_spawnChances;
    public GameObject[] obstacle_objects;
    public int[] obstacle_spawnChances;

    private GameObject[] spawner_objects;

    public float tick_max;
    public float tick_min;

    private float tick_rate;
    private float tick_time;
    private int tick_count;

    public float time_maxDifficulty;
    private float difficuty;

    public float wave_spawnChance;

    public float customer_spawnRate;

    public int fauxCustomer_spawnChance_normal;
    private int fauxCustomer_spawnChance_current;
    private int fauxCustomer_spawnChance_delta;

    public float object_spawnRate;

    public int pickup_spawnChance_normal;
    private int pickup_spawnChance_current;
    private int pickup_spawnChance_delta;
    private int pickup_spawnChance_bottom;



    //Function start

    private void Awake()
    {
        // Setting fox variables
        fauxCustomer_spawnChance_current = 0;
        fauxCustomer_spawnChance_delta = fauxCustomer_spawnChance_normal / 4;

        // Setting pickup variables
        pickup_spawnChance_bottom = pickup_spawnChance_normal / 2;
        pickup_spawnChance_current = pickup_spawnChance_bottom;
        pickup_spawnChance_delta = pickup_spawnChance_bottom / 4;

        // Setting tick
        tick_rate = tick_max;
        tick_time = tick_rate;

        spawner_objects = GameObject.FindGameObjectsWithTag("ObstacleSpawner");
        StartCoroutine("tick");
    }

    // Change tick over time to tick_min

    IEnumerator tick()
    {
        float customerDelay = GetDelay();
        float objectDelay = GetDelay();
        bool customerSpawned = false;
        bool objectSpawned = false;
        
        while (tick_time > 0)
        {
            if (customerDelay <= 0 && !customerSpawned)
            { SpawnCustomer(); customerSpawned = true; }
            else
                customerDelay -= Time.deltaTime;

            if (objectDelay <= 0 && !objectSpawned)
            { SpawnObject(); objectSpawned = true; }
            else
                objectDelay -= Time.deltaTime;


            tick_time -= Time.deltaTime;
            yield return null;
        }
        tick_time = tick_rate;
        StartCoroutine("tick");
    }

    float GetDelay()
    {
        return Random.Range(0, tick_rate);
    }

    int GetPercentage()
    {
        return Random.Range(0, 100);
    }

    Transform GetSpawnLane()
    {
        int lane = Random.Range(0, spawner_objects.Length);
        return spawner_objects[lane].transform;
    }

    // Note: Spawn Sheep or Fox
    void SpawnCustomer()
    {
        int spawn = GetPercentage();
        if (spawn <= customer_spawnRate)
        {
            Transform lane = GetSpawnLane();
            int fauxChance = GetPercentage();
            if(fauxChance <= fauxCustomer_spawnChance_current)
            {
                Instantiate(fauxCustomer_object, lane.position, Quaternion.identity);
                fauxCustomer_spawnChance_current = 0;
            }
            else
            {
                Instantiate(customer_object, lane.position, Quaternion.identity);
                if (fauxCustomer_spawnChance_current < fauxCustomer_spawnChance_normal * 2)
                    fauxCustomer_spawnChance_current += fauxCustomer_spawnChance_delta;
            }
        }
    }


    // Note: Spawn Pickup or Obstacle
    void SpawnObject()
    {
        int spawn = GetPercentage();
        if (spawn <= object_spawnRate)
        {
            Transform lane = GetSpawnLane();
            int PoO = GetPercentage();
            // Note: Spawn pickup
            if (PoO <= pickup_spawnChance_current)
            {
                int randomPickup = GetPercentage();
                GameObject obj = null;
                for (int i = 0; i < pickUp_objects.Length; i++)
                {
                    if (randomPickup < pickUp_spawnChances[i])
                    {
                        obj = pickUp_objects[i]; break;
                    }
                    else
                        randomPickup -= pickUp_spawnChances[i];
                }
                Instantiate(obj, lane.position, Quaternion.identity);
            }
            // Note: Spawn obstacle
            else
            {
                if (pickup_spawnChance_current < pickup_spawnChance_normal * 1.5)
                    pickup_spawnChance_current += pickup_spawnChance_delta;

                int randomObstacle = GetPercentage();
                GameObject obj = null;
                for (int i = 0; i < obstacle_objects.Length; i++)
                {
                    if (randomObstacle < obstacle_spawnChances[i])
                    {
                        obj = obstacle_objects[i]; break;
                    }
                    else
                        randomObstacle -= obstacle_spawnChances[i];
                }
                Instantiate(obj, lane.position, Quaternion.identity);

            }

        }
    }


    IEnumerator SpawnWave(int size)
    {
        while (size > 0)
        {
            List<GameObject> spawners = new List<GameObject>();
            for (int i = 0; i < spawner_objects.Length; i++)
            {
                spawners.Add(spawner_objects[i]);
            }
            for (int i = 0; i < spawner_objects.Length; i++)
            {
                if (size > 0)
                {
                    int spawner = Random.Range(0, spawners.Count);
                    Instantiate(customer_object, spawners[spawner].transform.position, Quaternion.identity);
                    spawners.RemoveAt(spawner);
                    size--;
                }

                float delay = .2f;
                while (delay > 0)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }
            }

            float row_delay = .5f;
            while (row_delay > 0)
            {
                row_delay -= Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }

    //   public float screenHeight;
    //   public float screenLow;

    //   public int fauxSpawnPercent;

    //   public Vector2 customerSpawnTime;
    //   public Vector2 obstacleSpawnTime;
    //   public Vector2 spawn_timeModifier_seconds;

    //   public GameObject customerObject;
    //   public GameObject fauxCustomerObject;

    //   public Vector2 pickUpsVsRoadObstacles;
    //   public GameObject[] roadObstacles;
    //   public GameObject[] pickUpObjects;
    //   public float[] roadObstaclesSpawnChance;
    //   public float[] pickUpSpawnChance;

    //   private float customerTimeCounter;
    //   private float obstacleTimeCounter;

    //   private GameObject[] objectSpawners;

    //   private float spawn_timeModifier;

    //   private float game_time = 0;
    //   [HideInInspector]
    //   public bool started = false;

    //   private IEnumerator wave_spawn;

    //   void Awake () {
    //       StartCoroutine("decreaseSpawnTimeModifier");

    //       customerTimeCounter = GetRandomSpawnTime(customerSpawnTime);
    //       obstacleTimeCounter = GetRandomSpawnTime(obstacleSpawnTime);

    //       objectSpawners = GameObject.FindGameObjectsWithTag("ObstacleSpawner");

    //       wave_spawn = SpawnWave(28);
    //       StartCoroutine(wave_spawn);
    //   }

    //void Update () {

    //       if (started)
    //       {
    //           game_time += Time.deltaTime;

    //           if (customerTimeCounter <= 0)
    //               SpawnSheep();
    //           else
    //               customerTimeCounter -= Time.deltaTime;

    //           if (obstacleTimeCounter <= 0)
    //               SpawnRoadObstacle();
    //           else
    //               obstacleTimeCounter -= Time.deltaTime;


    //           if (Mathf.Abs(obstacleTimeCounter - customerTimeCounter) < 0.25f)
    //               obstacleTimeCounter += 0.5f;
    //       }
    //   }

    //   float GetRandomSpawnTime(Vector2 minMax)
    //   {
    //       return Random.Range(minMax.x, minMax.y) * spawn_timeModifier;
    //   }

    //   void SpawnRoadObstacle()
    //   {
    //       float random = Random.Range(1, 101);

    //       if (random <= pickUpsVsRoadObstacles.x)
    //       {
    //           float pickUp_choice = Random.Range(1, 101);
    //           int numberToSpawn = 0;
    //           for (int i = 0; i < pickUpSpawnChance.Length; i++)
    //           {
    //               if (pickUp_choice <= pickUpSpawnChance[i])
    //               {
    //                   numberToSpawn = i;
    //                   break;
    //               }
    //               else
    //                   pickUp_choice -= pickUpSpawnChance[i];
    //           }



    //           int spawner_choice = Random.Range(0, objectSpawners.Length);

    //           Instantiate(pickUpObjects[numberToSpawn], objectSpawners[spawner_choice].transform.position, Quaternion.identity);
    //       }
    //       else
    //       {

    //           float object_choice = Random.Range(1, 101);
    //           int numberToSpawn = 0;
    //           for (int i = 0; i < roadObstaclesSpawnChance.Length; i++)
    //           {
    //               if (object_choice <= roadObstaclesSpawnChance[i])
    //               {
    //                   numberToSpawn = i;
    //                   break;
    //               }
    //               else
    //                   object_choice -= roadObstaclesSpawnChance[i];
    //           }
    //           int spawner_choice = Random.Range(0, objectSpawners.Length);
    //           Instantiate(roadObstacles[numberToSpawn], objectSpawners[spawner_choice].transform.position, Quaternion.identity);
    //       }
    //       obstacleTimeCounter = GetRandomSpawnTime(obstacleSpawnTime);
    //   }

    //   void SpawnSheep()
    //   {

    //       GameObject gameObjectToSpawn;
    //       int rand = Random.Range(1, 101);
    //       if (rand <= fauxSpawnPercent)
    //           gameObjectToSpawn = fauxCustomerObject;
    //       else
    //           gameObjectToSpawn = customerObject;


    //       float y_pos = Random.Range(screenLow, screenHeight);
    //       Vector2 spawnPos = new Vector2(11, y_pos);

    //       Instantiate(gameObjectToSpawn, spawnPos, Quaternion.identity);
    //       customerTimeCounter = GetRandomSpawnTime(customerSpawnTime);
    //   }

    //   private IEnumerator decreaseSpawnTimeModifier()
    //   {
    //       spawn_timeModifier = 1f;
    //       while (spawn_timeModifier > spawn_timeModifier_seconds.x)
    //       {
    //           spawn_timeModifier -= spawn_timeModifier_seconds.x / spawn_timeModifier_seconds.y;

    //           yield return null;
    //       }
    //   }


}
