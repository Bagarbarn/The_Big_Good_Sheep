using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour {

    public GameObject spriteObj;
    private SpriteRenderer sprite;
    private float barOffset;

    void Awake()
    {
        sprite = spriteObj.GetComponent<SpriteRenderer>();
        barOffset = 0;
    }

    public void SetPosition()
    {
        GameObject[] barCheck = GameObject.FindGameObjectsWithTag("PowerBar");
        if (barCheck.Length > 1){
            barOffset += (-0.2f * (barCheck.Length-1));
        }
        transform.localPosition = new Vector3(-0.4f, -1.3f+barOffset, 0);
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

    public void DestroyBar()
    {
        Destroy(gameObject);
    }
}
