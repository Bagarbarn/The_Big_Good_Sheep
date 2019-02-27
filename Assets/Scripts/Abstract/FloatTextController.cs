using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static FloatingText popupTextBad;
    private static GameObject canvas;
    private static Vector3 offset;

    static FloatTextController()
    {
        Initialize();
    }

    public static void Initialize()
    {
        canvas = GameObject.Find("UICanvas");
        popupText = Resources.Load<FloatingText>("PopupParent");
        popupTextBad = Resources.Load<FloatingText>("PopupParentBad");
    }

    public static void CreateFloatingText(string text, Transform location, bool good)
    {
        if (canvas == null)
            canvas = GameObject.Find("UICanvas");
        
        FloatingText instance;
        float add = 0;

        GameObject[] floatTextCheck = GameObject.FindGameObjectsWithTag("FloatingText");
        if (floatTextCheck.Length != 0)
            add += (0.5f * floatTextCheck.Length);
        offset.x = 0; offset.y = add; offset.z = 0;

        if (good == true)   instance = Instantiate(popupText);
        else                instance = Instantiate(popupTextBad);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + offset);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }

}
