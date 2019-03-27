using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScaling : MonoBehaviour {

    public float time;
    public float color_value_low;

    private float distance;

    public Image[] images = new Image[3];

    PauseMenu pause;

    public bool isPaused;

    public struct Holder
    {
        public Image image;
        public bool mouseOn;
    }

    Holder[] holders = new Holder[3];

    private void Start()
    {
        if (gameObject.tag == "UIButtons") isPaused = true;
        else
        pause = gameObject.GetComponent<PauseMenu>();
        distance = 1f - color_value_low;
        for (int i = 0; i < images.Length; i++)
        {
            holders[i] = new Holder();
            holders[i].mouseOn = false;
            holders[i].image = images[i];
        }
    }

    public void MouseOnResume()
    {
        holders[0].mouseOn = true;
        Debug.Log("Getting on Button");
    }

    public void MouseOffResume()
    {
        holders[0].mouseOn = false;
    }

    public void MouseOnMenu()
    {
        holders[1].mouseOn = true;
    }

    public void MouseOffMenu()
    {
        holders[1].mouseOn = false;
    }

    public void MouseOnQuit()
    {
        holders[2].mouseOn = true;
    }

    public void MouseOffQuit()
    {
        holders[2].mouseOn = false;
    }

    public void Update()
    {
        if (Time.timeScale == 1f && isPaused && gameObject.tag != "UIButtons")
        {
            isPaused = false;
            for (int i = 0; i < holders.Length; i++)
            {
                holders[i].mouseOn = false;
                holders[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else if (Time.timeScale == 0f && !isPaused)
        {
            isPaused = true;
            //GameObject[] go = GameObject.FindGameObjectsWithTag("PauseMenuImage");
            //for (int i = 0; i < holders.Length; i++)
            //{
            //    holders[i].image = go[i].GetComponent<Image>();
            //}
        }

        if (isPaused)
        {
            foreach (Holder holder in holders)
            {
                
                if (holder.mouseOn && holder.image.color.r > color_value_low)
                {
                    
                    float color_value = holder.image.color.r - (distance / time) * Time.unscaledDeltaTime;
                    if (color_value < color_value_low) color_value = color_value_low;
                    Color m_color = new Color(color_value, color_value, color_value, 1f);
                    holder.image.color = m_color;
                }
                else if (!holder.mouseOn && holder.image.color.r < 1f)
                {
                    float color_value = holder.image.color.r + (distance / time) * Time.unscaledDeltaTime;
                    if (color_value > 1f) color_value = 1f;
                    Color m_color = new Color(color_value, color_value, color_value, 1f);
                    holder.image.color = m_color;
                }
            }
        }
    }

}
