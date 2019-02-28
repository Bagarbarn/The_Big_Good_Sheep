using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour {

    public GameObject spriteObj;
    public SpriteRenderer sprite;

    void Awake()
    {
        sprite = spriteObj.GetComponent<SpriteRenderer>();
    }

    public void ChangeActive(bool active)
    {
        gameObject.SetActive(active);
    }

	public void SetFill(float percentage)
    {
        gameObject.transform.localScale = new Vector3(percentage, 1.0f);
    }

    public void SetColor(Color color)
    {
        sprite.color = color;
    }
}
