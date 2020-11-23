using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextScreenManager : MonoBehaviour
{
    public static TextScreenManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public TextMeshProUGUI screenText;
    public void Display(string str)
    {
        screenText.text = "<mspace={fontSize}>".Replace("{fontSize}", (ThemeManager.instance.selectedSize.fontSize * (5f / 6f)).ToString("0")) + CommandLineManager.instance.ConvertToConsoleText(str);
    }
}
