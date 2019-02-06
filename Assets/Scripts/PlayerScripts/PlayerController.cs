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


    public float p_speed;
    public float bullet_speed;
    public bool p_stunned;


    private float m_currentSpeed;

    private ColorManager colorManager;
    private Transform barrelEnd;


    //Debug Temp vars

    public string currentColor;


    [HideInInspector]
    public float p_slowPercentage;

    // Use this for initialization
    void Start() {
        p_stunned = false;
        m_currentSpeed = p_speed;
        colorManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>();
        barrelEnd = transform.Find("Barrel");
    }

    // Update is called once per frame
    void Update() {


        if (!p_stunned)
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


        if (Input.GetKeyDown(key_shoot))
        {
            GameObject scoop = colorManager.GetScoop();
            colorManager.Cancel();
            if (scoop != null)
            {
                Instantiate(scoop, barrelEnd.position, Quaternion.identity);
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

    public IEnumerator SetStunned(float time)
    {
        p_stunned = true;
        float time_left = time;
        while (time_left > 0)
        {
            time_left -= Time.deltaTime;

            yield return null;
        }
        p_stunned = false;

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


}
