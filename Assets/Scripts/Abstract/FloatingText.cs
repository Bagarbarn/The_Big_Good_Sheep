using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingText : MonoBehaviour {

    public Animator animator;
    private TextMeshProUGUI valueText;

    void Awake ()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        valueText = animator.GetComponent<TextMeshProUGUI>();
        Destroy(gameObject, clipInfo[0].clip.length-0.1f);
	}

    public void SetText(string text)
    {
        valueText.text = text;
    }
}
