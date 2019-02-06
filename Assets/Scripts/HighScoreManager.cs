﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public struct HighScoreData
{
    string m_name;
    int m_score;

    public string GetName(){
        return m_name;}
    
    public int GetScore(){
        return m_score;}

    public HighScoreData(string p_name, int p_score)
    {
        m_name = p_name;
        m_score = p_score;
    }

}

public class HighScoreManager : MonoBehaviour {

    protected static List<HighScoreData> scoreList = new List<HighScoreData>();
    protected static string path = "Assets/highscore.txt";
    public static int playerScore;
    public static string playerName;
    
    public Text[] highscoreSlots;
    public Text playerScoreSlot;
    public InputField nameInput;

    void Awake()
    {
        playerScore = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolderScript>().p_endScore;
        playerScoreSlot.text = "Your score: " + playerScore;

        ReadFile();
        UpdateList();
        WriteFile();
    }

    public void ReceiveSubmittedName()
    {
        playerName = nameInput.text;
        HighScoreData newPlayerScore = new HighScoreData(playerName, playerScore);
        scoreList.Add(newPlayerScore);
        UpdateList();
        WriteFile();
    }

    public void UpdateList()
    {
        scoreList.Sort((s1, s2) => -1 * s1.GetScore().CompareTo(s2.GetScore()));
        Debug.Log(scoreList.Count);
        if (highscoreSlots.Length <= scoreList.Count)
        {
            for (int i = 0; i < highscoreSlots.Length; i++){
                highscoreSlots[i].text = scoreList[i].GetName() + ": " + scoreList[i].GetScore();}}
        else {
            for (int i = 0; i < scoreList.Count; i++){
                highscoreSlots[i].text = scoreList[i].GetName() + ": " + scoreList[i].GetScore();
            }
        }
    }

    [MenuItem("Tools/Write file")]
    static void WriteFile()
    {
        StreamWriter writer = new StreamWriter(path, false);

        for (int i = 0; i < scoreList.Count; i++)
        {
            writer.WriteLine(scoreList[i].GetName());
            writer.WriteLine(scoreList[i].GetScore());
        }
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("highscore", typeof (TextAsset));
    }

    [MenuItem("Tools/Read file")]
    static void ReadFile()
    {
        StreamReader reader = new StreamReader(path);

        while (reader.EndOfStream == false)
        {
            string p_name = reader.ReadLine(); Debug.Log(p_name);
            string p_score_s = reader.ReadLine(); Debug.Log(p_score_s);
            int p_score = 0;

            if (int.TryParse(p_score_s, out p_score) == false){
                Debug.Log("Could not read score for " + p_name);}
            else {
                HighScoreData newScore = new HighScoreData(p_name, p_score);
                scoreList.Add(newScore);
            }
        }
        Debug.Log("--- COMPLETED FILE READING ---");
        reader.Close();
    }
}
