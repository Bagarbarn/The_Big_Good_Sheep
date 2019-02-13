using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_RoadblockScript : MonoBehaviour {

    public bool playerHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerHit = true;
        }
    }


}
