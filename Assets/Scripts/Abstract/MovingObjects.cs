using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObjects : MonoBehaviour {

    public float p_speed;

    [HideInInspector]
    public GameManagerScript gameManagerScript;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    // Use this for initialization
    public virtual void Start () {

        gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
    }
	
	// Update is called once per frame
	public virtual void Update () {

        transform.Translate(Vector2.left * (gameManagerScript.m_currentSpeed * Time.deltaTime + p_speed * Time.deltaTime));

	}
}
