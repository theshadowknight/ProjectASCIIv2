using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
[Serializable]
public class ThemeDictionary : SerializableDictionary<string, Theme> { }
[Serializable]
public class SizeDictionary : SerializableDictionary<string, FontSizeSettings> { }
public class ThemeManager : MonoBehaviour
{
    public static ThemeManager instance;
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
    public ThemeDictionary themes = new ThemeDictionary();
    public SizeDictionary sizes = new SizeDictionary();
    public string selectedThemestr;
    public string selectedSizestr;
    [SerializeReference]
    public Theme selectedTheme;
    [SerializeReference]
    public FontSizeSettings selectedSize;
    public Camera cam;
    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    public void SetupTheme()
    {

        cam.backgroundColor = selectedTheme.backgroundColor;
        foreach (TextMeshProUGUI tmp in texts)
        {
            tmp.color = selectedTheme.textColor;
        }
    }
    public void SetupTheme(string s)
    {

        Theme t = null;
        if (themes.TryGetValue(s, out t))
        {
            selectedTheme = t;

        }
        else
        {
            selectedTheme = themes.Values.ToArray()[0];
            selectedThemestr = themes.Keys.ToArray()[0];
        }
    }
    public void SetupSize()
    {


        foreach (TextMeshProUGUI tmp in texts)
        {
            tmp.fontSize = selectedSize.fontSize;
        }

        CommandLineManager.instance.UpdateText();
        StorageMemoryManager.instance.ChangePathText();
    }
    public void SetupSize(string s)
    {
        FontSizeSettings t = null;
        if (sizes.TryGetValue(s, out t))
        {
            selectedSize = t;
        }
        else
        {
            selectedSize = sizes.Values.ToArray()[0];
            selectedSizestr = sizes.Keys.ToArray()[0];

        }
    }
    public void Start()
    {
        SetupSelection();
        SetupAll();

        string k = "";
        for (int i = 0; i < selectedSize.lineCharSize; i++)
        {
            k += "n";
        }
        CommandLineManager.instance.Write(k);
    }
    public void SetupAll()
    {
        SetupTheme();
        SetupSize();
    }
    public void SetupSelection()
    {
        SetupTheme(selectedThemestr);
        SetupSize(selectedSizestr);
    }
    public void libne()
    {
        string k = "";
        for (int i = 0; i < selectedSize.lineCharSize; i++)
        {
            k += "n";
        }
        Debug.LogError(k);
       StartCoroutine( CommandLineManager.instance.Write(k));
    }
}
