using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public Text p_timeText;
    public float p_timeLimit;

    private float m_currentTime;

    [HideInInspector]
    public bool started;

	void Start () {
        m_currentTime = p_timeLimit;
        started = false;
	}
	
	void Update () {
        if (started)
            m_currentTime -= Time.deltaTime;
        p_timeText.text = "Time left: " + m_currentTime.ToString("F1");

        if (m_currentTime < 0)
        {
            Debug.Log("Time's up!");
            gameObject.GetComponent<GameManagerScript>().EndGame();
        }
    }

    public void AdjustTime(float timeToAdjust)
    {
        m_currentTime += timeToAdjust;
    }
}
