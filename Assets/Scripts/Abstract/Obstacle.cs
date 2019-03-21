using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MovingObjects {

    public bool pickup;

    public float speed_max = .2f;
    public float speed_addition = 0.05f;

    [HideInInspector]
    public int lane;

    public override void Start()
    {
        base.Start();
        //Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
        if (pickup)
            StartCoroutine("FloatingAnim");
    }

    public virtual void ObstacleEvent(GameObject playerObject)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            if (this.tag == "Obstacle")
            {
                if (other.gameObject.GetComponent<PlayerController>().invincible)
                    Destroy(this.gameObject);
                else
                    ObstacleEvent(other.gameObject);
            } else
                ObstacleEvent(other.gameObject);
        }
        else if (other.tag =="LeftBound")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator FloatingAnim()
    {
        string direction;
        float speed_current;
        if (Random.Range(0,2) == 1)
        {
            speed_current = speed_max;
            direction = "up";
        }
        else
        {
            speed_current = -speed_max;
            direction = "down";
        }
        
        while (true)
        {
            transform.Translate(Vector2.up * speed_current * Time.deltaTime);
            if (direction == "up")
            {

                speed_current -= speed_addition * Time.deltaTime;
                if (speed_current <= -speed_max)
                {
                    speed_current = -speed_max;
                    direction = "down";
                }
            }
            else if (direction == "down")
            {
                speed_current += speed_addition * Time.deltaTime;
                if (speed_current >= speed_max)
                {
                    speed_current = speed_max;
                    direction = "up";
                }
            }


            yield return null;
        }
    }

}
