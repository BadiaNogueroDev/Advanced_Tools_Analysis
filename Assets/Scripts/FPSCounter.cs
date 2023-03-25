using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{

    [HideInInspector]public int frameRate;
    public Text display_Text;

    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        frameRate = (int)current;
        display_Text.text = "FPS: " + frameRate.ToString();
    }
}
