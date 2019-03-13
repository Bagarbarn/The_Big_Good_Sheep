using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_RoadblockScript : MonoBehaviour {

    public bool playerHit;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerHit = true;
        }
    }


}
