using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    KeyCode key_moveUp = KeyCode.W;
    KeyCode key_moveDown = KeyCode.S;
    KeyCode key_shoot = KeyCode.Space;
    KeyCode key_colorOne = KeyCode.J;
    KeyCode key_colorTwo = KeyCode.K;
    KeyCode key_colorThree = KeyCode.L;
    KeyCode key_cancelColor = KeyCode.C;


    public AudioSource shootAudio;

    public float p_speed;
    public float bullet_speed;


    private float m_currentSpeed;

    private GameObject gameManager;
    private SoundScript soundScript;
    private ColorManager colorManager;
    private Transform barrelEnd;

    private bool m_stunned;
    private bool m_overdrive;

    //Debug Temp vars

    public string currentColor;


    [HideInInspector]
    public float p_slowPercentage;
    [HideInInspector]
    public float p_overdriveShotInterval;

    // Use this for initialization
    void Start() {
        m_stunned = false;
        m_overdrive = false;
        m_currentSpeed = p_speed;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        soundScript = gameManager.GetComponent<SoundScript>();
        colorManager = gameManager.GetComponent<ColorManager>();
        barrelEnd = transform.Find("Barrel");
    }

    // Update is called once per frame
    void Update() {
        if (!m_stunned)
            GetInput();
    }

    void GetInput()
    {
        if (transform.position.y < 4)
            if (Input.GetKey(key_moveUp))
                transform.Translate(Vector2.up * m_currentSpeed * Time.deltaTime);
        if (transform.position.y > -4)
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

    public IEnumerator Overdrive(float time)
    {
        m_overdrive = true;
        colorManager.Cancel();
        float time_left = time;
        float shotCD = 0;
        //Get Rainbow Scoop
        GameObject rainbowScoop =  colorManager.GetRainbowScoop();
        while (time_left > 0)
        {
            time_left -= Time.deltaTime;
            if (shotCD <= 0)
            {
                Shoot(rainbowScoop);
                shotCD = p_overdriveShotInterval;
            } else
                shotCD -= Time.deltaTime;

            yield return null;
        }
        m_overdrive = false;
    }
}
