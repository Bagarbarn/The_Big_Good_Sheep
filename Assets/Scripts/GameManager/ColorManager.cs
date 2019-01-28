using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    string[] colorList = { "red", "yellow", "blue", "orange", "green", "purple" };

    string BaseToMixed(string first, string second)
    {

        if ((first == "red" && second == "blue") || (first == "blue" && second == "red"))
            return "purple";
        else if ((first == "yellow" && second == "blue") || (first == "blue" && second == "yellow"))
            return "green";
        else if ((first == "red" && second == "yellow") || (first == "yellow" && second == "red"))
            return "orange";
        else return "null";
    }

    public string GetRandomColor()
    {

        int rand = Random.Range(0, colorList.Length);
        return colorList[rand];

    }


    public Sprite[] sprites;

    Sprite GetSprite(string color)
    {
        switch (color)
        {
            case "red":
                return sprites[0];
                break;
            case "blue":
                return sprites[1];
                break;
            case "yellow":
                return sprites[2];
                break;
            case "orange":
                return sprites[3];
                break;
            case "green":
                return sprites[4];
                break;
            case "purple":
                return sprites[5];
                break;
            default: return sprites[0];
                break;
        }
    }


    public GameObject[] bullets;

    GameObject GetBullet(string color)
    {
        switch (color)
        {
            case "red":
                return bullets[0];
                break;
            case "blue":
                return bullets[1];
                break;
            case "yellow":
                return bullets[2];
                break;
            case "orange":
                return bullets[3];
                break;
            case "green":
                return bullets[4];
                break;
            case "purple":
                return bullets[5];
                break;
            default: return bullets[0];
                break;
        }
    }
}
