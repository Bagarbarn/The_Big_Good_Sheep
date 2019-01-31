using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MovingObjects {


    public virtual void ObstacleEvent()
    {
        //this is for the object specific script;
        Debug.Log("Au, I'm Hit");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            ObstacleEvent();

        }
    }

}
