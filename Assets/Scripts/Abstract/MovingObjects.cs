using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObjects : MonoBehaviour {

    public float p_speed;

    [HideInInspector]
    public GameManagerScript gameManagerScript;



	// Use this for initialization
	public virtual void Start () {

        gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
	}
	
	// Update is called once per frame
	public virtual void Update () {

        transform.Translate(Vector2.left * (gameManagerScript.m_currentSpeed * Time.deltaTime + p_speed * Time.deltaTime));

	}
}
