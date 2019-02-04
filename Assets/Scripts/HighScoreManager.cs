using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class HighScoreManager : MonoBehaviour {

    public Text[] highscoreSlots;

    void Awake()
    {
        WriteString();
    }

    [MenuItem("Tools/Write file")]
    static void WriteString()
    {
        string path = "Assets/highscore.txt";

        StreamWriter writer = new StreamWriter(path, false);

        for (int i = 0; i < 5; i++)
        {
            writer.WriteLine("Tommy");
            writer.WriteLine("77");
        }
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("highscore", typeof (TextAsset));

        ReadString();
    }

    [MenuItem("Tools/Read file")]
    static void ReadString()
    {
        string path = "Assets/highscore.txt";

        StreamReader reader = new StreamReader(path);

        while (reader.EndOfStream == false)
        {
            Debug.Log(reader.ReadLine());
        }
        
        //reader.ReadToEnd();
        reader.Close();
    }
}
