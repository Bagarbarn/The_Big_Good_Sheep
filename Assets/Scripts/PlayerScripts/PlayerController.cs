using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float p_speed;


    KeyCode key_moveUp = KeyCode.W;
    KeyCode key_moveDown = KeyCode.S;
    KeyCode key_shoot = KeyCode.Space;
    KeyCode key_colorOne = KeyCode.J;
    KeyCode key_colorTwo = KeyCode.K;
    KeyCode key_colorThree = KeyCode.L;



    //Debug Temp vars

    public string currentColor;

	// Use this for initialization
	void Start () {
		
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
            currentColor = "Shoot"; //Shoot

        if (Input.GetKeyDown(key_colorOne))
            currentColor = "Red"; //colorManager.SetColor("Red");
        if (Input.GetKeyDown(key_colorTwo))
            currentColor = "Blue"; //colorManager.SetColor("Blue");
        if (Input.GetKeyDown(key_colorThree))
            currentColor = "Yellow"; //colorManager.SetColor("Yellow");

    }
}
