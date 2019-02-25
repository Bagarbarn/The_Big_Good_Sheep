using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour {

    public bool t_gameEnd;

    public float p_startTimer;

    public Image countdownImage;
    public Sprite[] countdownSprites;

    public float p_minSpeed;
    public float p_maxSpeed;
    public float p_maxTimer;


    public Text p_scoreText;


    private int m_score;
    private float m_acceleration;


    [HideInInspector]
    public float m_currentSpeed;

    private bool started;

	void Awake () {

        m_acceleration = (p_maxSpeed - p_minSpeed) / p_maxTimer;
        m_currentSpeed = p_minSpeed;

        p_scoreText.text = "Score: " + m_score;
        started = false;
        StartCoroutine("StartGameCountdown");
    }
	
	void Update () {

        if (started)
        {
            if (m_currentSpeed < p_maxSpeed)
                m_currentSpeed += m_acceleration * Time.deltaTime;
        }
    }

    IEnumerator StartGameCountdown()
    {
        countdownImage.sprite = countdownSprites[3];
        countdownImage.enabled = true;
        while (p_startTimer >= 0)
        {
            if (p_startTimer <= 2)
                countdownImage.sprite = countdownSprites[2];
            if (p_startTimer <= 1)
                countdownImage.sprite = countdownSprites[1];
            p_startTimer -= Time.deltaTime;
            yield return null;
        }
        countdownImage.sprite = countdownSprites[0];
        gameObject.GetComponent<TimeManager>().started = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().started = true;
        //gameObject.GetComponent<SpawnManager>().started = true;
        started = true;
        while(p_startTimer >= -1)
        {
            p_startTimer -= Time.deltaTime;
            yield return null;
        }
        countdownImage.enabled = false;
    }

    public void AddScore(int scoreToAdd)
    {
        m_score += scoreToAdd;
        p_scoreText.text = "Score: " + m_score;
    }

    public void EndGame()
    {
        GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolderScript>().p_endScore = m_score;
        Debug.Log("Times up! \nWait... Am I supposed to do something here?");
        if (t_gameEnd)
            SceneManager.LoadScene("ScoreBoard");
    }
}
