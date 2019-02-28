using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour {

    public Canvas canvas;

    string[] colorList = { "red", "yellow", "blue", "orange", "green", "purple" };

    string player_firstColor;
    string player_secondColor;

    public GameObject rainbowBullet;

    public GameObject[] bullets;
    public Sprite[] sprites;

    private Image scoopImageOne;
    private Image scoopImageTwo;
    private Image scoopImageThree;

    private Color color_red = new Color(232f/255f, 108f/255f, 96f/255f, 1.0f);
    private Color color_blue = new Color(124f/255f, 156f/255f, 205f/255f, 1.0f);
    private Color color_yellow = new Color(243f/255f, 236f/255f, 139f/255f, 1.0f);

    private Color color_orange = new Color(219f/255f, 155f/255f, 47f/255, 1.0f);
    private Color color_green = new Color(157f/255f, 191f/255f, 52f/255f, 1.0f);
    private Color color_purple = new Color(197f/255f, 128f/255f, 183f/255f, 1.0f);

    private void Start()
    {
        scoopImageOne = canvas.transform.Find("IceCreamOne").GetComponent<Image>();
        scoopImageTwo = canvas.transform.Find("IceCreamTwo").GetComponent<Image>();
        scoopImageThree = canvas.transform.Find("IceCreamThree").GetComponent<Image>();
    }

    public GameObject GetRainbowScoop()
    {
        return rainbowBullet;
    }

    public void SelectColor(string color)
    {
        if (player_firstColor == null)
        {
            player_firstColor = color;
            scoopImageOne.gameObject.SetActive(true);
            scoopImageOne.color = GetColor(color);
            scoopImageThree.gameObject.SetActive(true);
            scoopImageThree.color = GetColor(color);
        }
        else if (player_secondColor == null)
        {
            player_secondColor = color;
            scoopImageTwo.gameObject.SetActive(true);
            scoopImageTwo.color = GetColor(color);
            scoopImageThree.gameObject.SetActive(true);
            scoopImageThree.color = GetColor(BaseToMixed(player_firstColor, player_secondColor));
        }



        //Debug.Log(player_secondColor + ", " + player_firstColor);
    }

    public GameObject GetScoop()
    {
        string end_color;
        if ((player_firstColor != null && player_secondColor != null) && player_firstColor != player_secondColor)
            end_color = BaseToMixed(player_firstColor, player_secondColor);
        else if (player_firstColor != null)
            end_color = player_firstColor;
        else
            return null;

        player_firstColor = null;
        player_secondColor = null;

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
        else if (first == second)
            return first;
        else return "null";
    }

    public string GetRandomColor(bool multicolor)
    {
        int rand = 0;
        if(!multicolor)
            rand = Random.Range(0, 3);
        else
            rand = Random.Range(0, colorList.Length);
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
                return color_red;
            case "blue":
                return color_blue;
            case "yellow":
                return color_yellow;
            case "orange":
                return color_orange;
            case "green":
                return color_green;
            case "purple":
                return color_purple;
            default: return color_red;
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

    public void Cancel()
    {
        player_firstColor = null;
        player_secondColor = null;
        scoopImageOne.gameObject.SetActive(false);
        scoopImageTwo.gameObject.SetActive(false);
        scoopImageThree.gameObject.SetActive(false);

    }
}


//GitLab