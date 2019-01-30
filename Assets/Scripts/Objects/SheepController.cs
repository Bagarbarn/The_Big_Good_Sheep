using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MovingObjects {



    private string m_demandedColor = "red";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<BulletScript>().p_color == m_demandedColor)
            {
                Destroy(other);
                Destroy(gameObject);
            }
        }
    }


}


