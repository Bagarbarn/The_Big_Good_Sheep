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


    public float p_speed;
    public float bullet_speed;


    private ColorManager colorManager;
    private Transform barrelEnd;


    //Debug Temp vars

    public string currentColor;

	// Use this for initialization
	void Start () {
        colorManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>();
        barrelEnd = transform.Find("Barrel");
	}
	
	// Update is called once per frame
	void Update () {

        GetInput();


	}

    void GetInput()
    {
        if (transform.position.y < 4)
            if (Input.GetKey(key_moveUp))
                transform.Translate(Vector2.up * p_speed * Time.deltaTime);
        if (transform.position.y > -4)
            if (Input.GetKey(key_moveDown))
                transform.Translate(-Vector2.up * p_speed * Time.deltaTime);


        if (Input.GetKeyDown(key_shoot))
        {
            GameObject scoop = colorManager.GetScoop();
            if (scoop != null)
            {
                GameObject icecream = Instantiate(scoop, barrelEnd.position, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(key_colorOne))
            colorManager.SelectColor("red");
        if (Input.GetKeyDown(key_colorTwo))
            colorManager.SelectColor("blue");
        if (Input.GetKeyDown(key_colorThree))
            colorManager.SelectColor("yellow");

    }
}
