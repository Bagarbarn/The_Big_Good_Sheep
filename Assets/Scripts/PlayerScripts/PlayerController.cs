using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [HideInInspector]
    public KeyCode key_moveUp = KeyCode.W;
    [HideInInspector]
    public KeyCode key_moveDown = KeyCode.S;
    [HideInInspector]
    public KeyCode key_shoot = KeyCode.Space;
    [HideInInspector]
    public KeyCode key_colorOne = KeyCode.J;
    [HideInInspector]
    public KeyCode key_colorTwo = KeyCode.K;
    [HideInInspector]
    public KeyCode key_colorThree = KeyCode.L;
    [HideInInspector]
    public KeyCode key_cancelColor = KeyCode.C;


    public AudioSource shootAudio;

    public float p_speed;
    public float bullet_speed;



    [HideInInspector]
    public bool invincible;

    private float m_currentSpeed;

    private GameObject gameManager;
    private ColorManager colorManager;
    private Transform barrelEnd;
    private SpriteRenderer spriteRenderer;

    private bool m_stunned;
    private bool m_overdrive;

    private GameObject invincibilityStar;
    private GameObject powerBar;

    //Debug Temp vars

    public string currentColor;


    [HideInInspector]
    public float p_slowPercentage;
    [HideInInspector]
    public float p_boostPercentage;
    [HideInInspector]
    public float p_overdriveShotInterval;
    //[HideInInspector]
    public bool started;

    // Use this for initialization
    void Start() {
        invincible = false;
        m_stunned = false;
        m_overdrive = false;
        started = false;
        m_currentSpeed = p_speed;

        gameManager = GameObject.FindGameObjectWithTag("GameController");
        colorManager = gameManager.GetComponent<ColorManager>();
        
        barrelEnd = transform.Find("Barrel");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        powerBar = Resources.Load<GameObject>("PowerBar");

        invincibilityStar = transform.GetChild(2).gameObject;
        invincibilityStar.SetActive(false);

        key_cancelColor = KeyCode.I;
    }

    // Update is called once per frame
    void Update() {
        if (!m_stunned && started)
            GetInput();
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
    }

    void GetInput()
    {
        if (transform.position.y < 3.5)
            if (Input.GetKey(key_moveUp))
                transform.Translate(Vector2.up * m_currentSpeed * Time.deltaTime);
        if (transform.position.y > -2.1)
            if (Input.GetKey(key_moveDown))
                transform.Translate(-Vector2.up * m_currentSpeed * Time.deltaTime);

        if (!m_overdrive)
        {
            if (Input.GetKeyDown(key_shoot))
            {
                GameObject scoop = colorManager.GetScoop();
                colorManager.Cancel();
                if (scoop != null)
                {
                    Shoot(scoop);
                }
            }

            if (Input.GetKeyDown(key_colorOne))
                colorManager.SelectColor("red");
            if (Input.GetKeyDown(key_colorTwo))
                colorManager.SelectColor("blue");
            if (Input.GetKeyDown(key_colorThree))
                colorManager.SelectColor("yellow");

            if (Input.GetKeyDown(key_cancelColor))
                colorManager.Cancel();
        }
    }

    void Shoot(GameObject bullet)
    {
        Instantiate(bullet, barrelEnd.position, Quaternion.identity);
        shootAudio.Play();
    }

    public IEnumerator SetStunned(float time)
    {
        m_stunned = true;
        float time_left = time;
        while (time_left > 0)
        {
            time_left -= Time.deltaTime;
            yield return null;
        }
        m_stunned = false;
    }

    public IEnumerator SetSlowed(float time)
    {
        m_currentSpeed = p_speed * (1 - p_slowPercentage/100);
        float time_left = time;
        while (time_left > 0)
        {
            time_left -= Time.deltaTime;
            yield return null;
        }
        m_currentSpeed = p_speed;
    }

    public IEnumerator SetBoost(float time)
    {
        m_currentSpeed = p_speed * (1 + p_boostPercentage / 100);
        
        GameObject newBar = Instantiate(powerBar);
        newBar.transform.parent = gameObject.transform;
        PowerBar newBarScript = newBar.GetComponent<PowerBar>();
        newBarScript.SetPosition();

        newBarScript.ChangeActive(true);
        newBarScript.SetColor(colorManager.GetColor("orange"));

        float time_left = time;
        while (time_left > 0)
        {
            time_left -= Time.deltaTime;
            newBarScript.SetFill(time_left / time);
            yield return null;
        }
        m_currentSpeed = p_speed;
        newBarScript.DestroyBar();
    }

    public IEnumerator Overdrive(float time)
    {
        GameObject newBar = Instantiate(powerBar);
        PowerBar newBarScript = newBar.GetComponent<PowerBar>();
        newBar.transform.parent = gameObject.transform;
        newBarScript.SetPosition();

        m_overdrive = true;
        newBarScript.ChangeActive(true);
        newBarScript.SetColor(colorManager.GetColor("green"));
        colorManager.Cancel();
        float time_left = time;
        float shotCD = 0;
        //Get Rainbow Scoop
        GameObject rainbowScoop =  colorManager.GetRainbowScoop();
        while (time_left > 0)
        {
            time_left -= Time.deltaTime;
            newBarScript.SetFill(time_left / time);
            if (shotCD <= 0)
            {
                Shoot(rainbowScoop);
                shotCD = p_overdriveShotInterval;
            } else
                shotCD -= Time.deltaTime;

            yield return null;
        }
        m_overdrive = false;
        newBarScript.DestroyBar();
    }

    public IEnumerator Invincibility(float time)
    {
        invincible = true;
        GameObject newBar = Instantiate(powerBar);
        PowerBar newBarScript = newBar.GetComponent<PowerBar>();
        newBar.transform.parent = gameObject.transform;
        newBarScript.SetPosition();

        invincibilityStar.SetActive(true);
        newBarScript.SetColor(colorManager.GetColor("purple"));

        float time_left = time;
        float blink_start = time / 5;
        float blink_interval = .5f;
        float blink_timer = 0;
        bool on = true;

        while (time_left > 0)
        {
            if (time_left <= blink_start)
            {
                if (blink_timer <= 0)
                {
                    if (on)
                    {
                        invincibilityStar.SetActive(false);
                        on = false;
                        blink_timer = blink_interval;
                    }
                    else
                    {
                        invincibilityStar.SetActive(true);
                        on = true;
                        blink_timer = blink_interval;
                    }
                }
                blink_timer -= Time.deltaTime;
            }
            time_left -= Time.deltaTime;
            newBarScript.SetFill(time_left / time);
            yield return null;
        }
        invincible = false;
        invincibilityStar.SetActive(false);
        newBarScript.DestroyBar();
    }

}
