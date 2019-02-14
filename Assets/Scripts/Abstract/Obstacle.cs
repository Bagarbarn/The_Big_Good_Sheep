using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MovingObjects {




    public override void Start()
    {
        base.Start();
        Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
    }

    public virtual void ObstacleEvent(GameObject playerObject)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            ObstacleEvent(other.gameObject);
        }
    }

}
