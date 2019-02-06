using System.Collections;
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
    public Text[] highscoreSlots;

    void Awake()
    {
        playerScore = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolderScript>().p_endScore;
        ReadFile();

        for (int i = 0; i < highscoreSlots.Length; i++)
        {
            highscoreSlots[i].text = scoreList[i].GetName() + ": " + scoreList[i].GetScore();
        }

        WriteFile();
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
        HighScoreData newPlayerScore = new HighScoreData("Player", playerScore);
        scoreList.Add(newPlayerScore);

        Debug.Log("--- COMPLETED LIST ---");

        scoreList.Sort((s1, s2) => -1* s1.GetScore().CompareTo(s2.GetScore()));

        for (int i = 0; i < scoreList.Count; i++){
            Debug.Log(scoreList[i].GetName() + ": " + scoreList[i].GetScore());
        }
        reader.Close();
    }
}
