using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    struct Event
    {
        public Event(float _time, string _name) { time = _time; name = _name; active = true; }
        public float time;
        public string name;
        public bool active;
    }


    public GameObject customer_object;
    public GameObject fauxCustomer_object;
    public GameObject[] pickUp_objects;
    public int[] pickUp_spawnChances;
    public GameObject[] obstacle_objects;
    public int[] obstacle_spawnChances;

    private GameObject[] spawner_objects;


    public float[] event_tsunami;

    Event[] events;


    public float tick_max;
    public float tick_min;

    public float tick_rate;
    private float tick_delta;
    private float tick_time;
    private int tick_count;

    public float time_maxDifficulty;
    public float difficulty;

    public float difficulty_multicolor;
    private bool spawn_multicolor;

    public int wave_difficulty;
    public float wave_spawnChance;
    public int wave_minSize;
    public int wave_maxSize;

    public int customer_spawnRate;
    private int customer_spawnRate_current;
    private int customer_spawnRate_bottom;
    private int customer_spawnRate_delta;

    public int fauxCustomer_spawnChance_normal;
    private int fauxCustomer_spawnChance_current;
    private int fauxCustomer_spawnChance_delta;

    public int object_spawnRate;

    public int pickup_spawnChance_normal;
    private int pickup_spawnChance_current;
    private int pickup_spawnChance_delta;
    private int pickup_spawnChance_bottom;

    private IEnumerator wave_coroutine;
    private bool waving;
    private bool eventing;

    //Function start

    private void Awake()
    {
        eventing = false;
        waving = false;
        spawn_multicolor = false;

        // Note: Setting fox variables
        fauxCustomer_spawnChance_current = 0;
        fauxCustomer_spawnChance_delta = fauxCustomer_spawnChance_normal / 4;

        // Note: Setting pickup variables
        pickup_spawnChance_bottom = pickup_spawnChance_normal / 2;
        pickup_spawnChance_current = pickup_spawnChance_bottom;
        pickup_spawnChance_delta = pickup_spawnChance_bottom / 4;

        // Note: Setting tick
        tick_rate = tick_max;
        tick_time = tick_rate;
        tick_delta = (tick_max - tick_min) / time_maxDifficulty;

        // Note: Setting Event list
        int event_listLength = event_tsunami.Length;
        events = new Event[event_listLength];

        // Note: Manually add all events
        int step_current = 0;
        int step_previous = 0;
        int step_goal = 0;

        // Note: Add tsunami
        step_goal += event_tsunami.Length;
        for ( ; step_current < step_goal; step_current++)
        { events[step_current] = new Event(event_tsunami[step_current - step_previous], "Tsunami");  }
        step_previous = step_goal;

        // Note: Add events like this

        step_goal += /*event_name*/ 404;
        //for (; step_current < step_goal; step_current++)
        //{ events[step_current] = new Event (404, " "); }


        spawner_objects = GameObject.FindGameObjectsWithTag("ObstacleSpawner");

        // Note: Game Start Routines
        StartCoroutine("tick");
        StartCoroutine("AdjustTick");
        StartCoroutine("IncreaseDifficulty");
    }

    private void Update()
    {
        if (difficulty >= difficulty_multicolor)
            spawn_multicolor = true;

        for (int i = 0; i < events.Length; i++)
        {
            if (events[i].active)
            {
                if (events[i].time > 0)
                    events[i].time -= Time.deltaTime;
                else
                { StartCoroutine(events[i].name); events[i].active = false; }
            }
        }
    }

    // Change tick over time to tick_min
    IEnumerator IncreaseDifficulty()
    {
        while (difficulty < 100)
        {
            difficulty += (100 / time_maxDifficulty) * Time.deltaTime;
            yield return null;
        }
        if (difficulty > 100)
            difficulty = 100;
    }


    IEnumerator AdjustTick()
    {
        while (tick_rate > tick_min)
        {
            tick_rate -= tick_delta * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator tick()
    {
        float customerDelay = GetDelay();
        float objectDelay = GetDelay();
        bool customerSpawned = false;
        bool objectSpawned = false;
        

        while (tick_time > 0)
        {
            if (!waving && !eventing)
            {
                if (customerDelay <= 0 && !customerSpawned)
                { SpawnCustomer(); customerSpawned = true; }
                else
                    customerDelay -= Time.deltaTime;

                if (objectDelay <= 0 && !objectSpawned)
                { SpawnObject(); objectSpawned = true; }
                else
                    objectDelay -= Time.deltaTime;

            }

            tick_time -= Time.deltaTime;
            yield return null;
        }
        tick_time = tick_rate;
        StartCoroutine("tick");
    }

    public bool GetColorMode()
    {
        return spawn_multicolor;
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
    void SpawnCustomer(bool spawn = false, bool obstacle = false, bool pickup = false)
    {
        if (GetPercentage() <= customer_spawnRate)
        {
            if (GetPercentage() <= wave_spawnChance && difficulty > wave_difficulty)
            {
                wave_coroutine = SpawnWave(Random.Range(wave_minSize, wave_maxSize));
                StartCoroutine(wave_coroutine);
            }
            else
            {
                Transform lane = GetSpawnLane();
                float y_offset = Random.Range(-.5f, 1f);
                Vector3 spawn_pos = new Vector3(lane.position.x, lane.position.y + y_offset, lane.position.z);
                if (GetPercentage() <= fauxCustomer_spawnChance_current)
                {
                    Instantiate(fauxCustomer_object, spawn_pos, Quaternion.identity);                
                    fauxCustomer_spawnChance_current = 0;
                }
                else
                {
                    Instantiate(customer_object, spawn_pos, Quaternion.identity);
                    if (fauxCustomer_spawnChance_current < fauxCustomer_spawnChance_normal * 2)
                        fauxCustomer_spawnChance_current += fauxCustomer_spawnChance_delta;
                }
            }
            
        }
    }


    // Note: Spawn Pickup or Obstacle
    void SpawnObject(bool spawn = false)
    {
        if (GetPercentage() <= object_spawnRate || spawn)
        {
            Transform lane = GetSpawnLane();
            // Note: Spawn pickup
            if (GetPercentage() <= pickup_spawnChance_current)
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
        // Note: Just smile and wave
        waving = true;
        while (size > 0)
        {
            List<GameObject> spawners = new List<GameObject>();
            for (int i = 0; i < spawner_objects.Length; i++)
            {
                spawners.Add(spawner_objects[i]);
            }

            int onRow = Random.Range(1, 3);

            for (int i = 0; i < onRow; i++)
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
        waving = false;
    }

    IEnumerator ObstacleCourse()
    {
        while (waving)
            yield return null;

        eventing = true;
        Debug.Log("Running obstacle course");
        while (eventing)
        {


            yield return null;
        }
        eventing = false;
    }

    IEnumerator Tsunami()
    {
        while (waving)
            yield return null;

        eventing = true;
        float time_waveDelay = 1.0f;
        float timer;
        Debug.Log("Tsunami");

        IEnumerator wave = SpawnWave(2);
        StartCoroutine(wave);
        while (waving)
            yield return null;
        timer = time_waveDelay;
        while (timer > 0)
        { timer -= Time.deltaTime; yield return null; }

        wave = SpawnWave(3);
        StartCoroutine(wave);
        while (waving)
            yield return null;
        timer = time_waveDelay;
        while (timer > 0)
        { timer -= Time.deltaTime; yield return null; }

        wave = SpawnWave(5);
        StartCoroutine(wave);
        while (waving)
            yield return null;
        timer = time_waveDelay;
        while (timer > 0)
        { timer -= Time.deltaTime; yield return null; }

        wave = SpawnWave(3);
        StartCoroutine(wave);
        while (waving)
            yield return null;
        timer = time_waveDelay;
        while (timer > 0)
        { timer -= Time.deltaTime; yield return null; }

        wave = SpawnWave(2);
        StartCoroutine(wave);
        while (waving)
            yield return null;

        eventing = false;
    }
}
