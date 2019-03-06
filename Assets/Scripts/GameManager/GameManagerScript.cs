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
    public Text p_comboText;
    public Text p_multiplyText;

    [HideInInspector]
    public float m_multiplier;
    private int m_score;
    private int m_comboCount;
    private float m_acceleration;


    [HideInInspector]
    public float m_currentSpeed;

    private bool started;

	void Awake () {

        m_acceleration = (p_maxSpeed - p_minSpeed) / p_maxTimer;
        m_currentSpeed = p_minSpeed;

        p_scoreText.text = "Score: " + m_score;
        m_multiplier = 1.0f;
        p_multiplyText.text = "Score x" + m_multiplier;
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
        if (scoreToAdd > 0){
            m_comboCount += 1;
            p_comboText.gameObject.SetActive(true);
            p_comboText.text = "Combo x" + m_comboCount;
        }
        else if (scoreToAdd < 0) {
            BreakCombo();
        }

        if (m_comboCount == 10)
            m_multiplier = 1.5f;
        else if (m_comboCount % 10 == 0 && m_comboCount > 10)
            m_multiplier = m_comboCount / 10;
        else if (m_multiplier >= 10)
            m_multiplier = 10;
        p_multiplyText.text = "Score x" + m_multiplier;

        if (scoreToAdd >= 10 || m_multiplier >= 2.0f){
            float multipliedScore = (float)scoreToAdd * m_multiplier;
            scoreToAdd = (int)multipliedScore;
        }

        m_score += scoreToAdd;
        p_scoreText.text = "Score: " + m_score;
    }

    public void BreakCombo()
    {
        m_comboCount = 0;

        if (m_multiplier > 3.0f)
            m_multiplier -= 2.0f;
        else if (m_multiplier == 3.0f)
            m_multiplier = 1.5f;
        else m_multiplier = 1.0f;

        p_multiplyText.text = "Score x" + m_multiplier;
        p_comboText.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolderScript>().p_endScore = m_score;
        Debug.Log("Times up! \nWait... Am I supposed to do something here?");
        if (t_gameEnd)
            SceneManager.LoadScene("ScoreBoard");
    }
}
