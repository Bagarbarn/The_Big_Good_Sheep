using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_OverdriveScript : MonoBehaviour {

    public float time;
    public float overdriveShotSpeed;
    private IEnumerator coroutine;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            coroutine = other.gameObject.GetComponent<PlayerController>().Overdrive(time);
            other.gameObject.GetComponent<PlayerController>().p_overdriveShotInterval = overdriveShotSpeed;
            other.gameObject.GetComponent<PlayerController>().StartCoroutine(coroutine);
            Destroy(this.gameObject);
        }
    }
}
