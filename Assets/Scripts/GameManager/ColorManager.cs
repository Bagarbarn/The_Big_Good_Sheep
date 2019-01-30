﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    string[] colorList = { "red", "yellow", "blue", "orange", "green", "purple" };

    string player_firstColor;
    string player_secondColor;


    public GameObject[] bullets;
    public Sprite[] sprites;

    private Color color_purple = new Color(255, 0, 255, 255);
    private Color color_orange = new Color(1.0f, 0.64f, 0f, 1f);

    public void SelectColor(string color)
    {
        if (player_firstColor == null)
            player_firstColor = color;
        else if (player_secondColor == null)
            player_secondColor = color;

        Debug.Log(player_secondColor + ", " + player_firstColor);
    }

    public GameObject GetScoop()
    {
        Debug.Log("Getting Scoop");
        string end_color;
        if ((player_firstColor != null && player_secondColor != null) && player_firstColor != player_secondColor)
            end_color = BaseToMixed(player_firstColor, player_secondColor);
        else if (player_firstColor != null)
            end_color = player_firstColor;
        else
            return null;

        player_firstColor = null;
        player_secondColor = null;

        Debug.Log(end_color);

        return GetBullet(end_color);

    }


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




    public Sprite GetSprite(string color)
    {
        switch (color)
        {
            case "red":
                return sprites[0];
            case "blue":
                return sprites[1];
            case "yellow":
                return sprites[2];
            case "orange":
                return sprites[3];
            case "green":
                return sprites[4];
            case "purple":
                return sprites[5];
            default: return sprites[0];
        }
    }


    public Color GetColor(string color)
    {
        switch (color)
        {
            case "red":
                return Color.red;
            case "blue":
                return Color.blue;
            case "yellow":
                return Color.yellow;
            case "orange":
                return color_orange;
            case "green":
                return Color.green;
            case "purple":
                return color_purple;
            default: return Color.red;
        }
    }



    GameObject GetBullet(string color)
    {
        switch (color)
        {
            case "red":
                return bullets[0];
            case "blue":
                return bullets[1];
            case "yellow":
                return bullets[2];
            case "orange":
                return bullets[3];
            case "green":
                return bullets[4];
            case "purple":
                return bullets[5];
            default: return bullets[0];
        }
    }
}


//GitLab