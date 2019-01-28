using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float p_speed;





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        PlayerMove();



	}

    void PlayerMove()
    {
        
            if (transform.position.y < 4)
                if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * p_speed * Time.deltaTime);
            }
            if (transform.position.y > -4)
                if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector2.up * p_speed * Time.deltaTime);
            }
    }
}
