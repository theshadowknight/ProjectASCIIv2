using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class CodeConverterDictionary : SerializableDictionary<string, string> { }

public class CommandLineManager : MonoBehaviour
{
    public TextMeshProUGUI TMP;

    public CodeConverterDictionary convertingTextDictionary = new CodeConverterDictionary();
    public List<Color> col = new List<Color>();
    
    // [Multiline(6)]
    public string cnslTxt;
    public string consoleText
    {
        get
        {
            return ConvertFromConsoleText(cnslTxt);
        }
        set
        {
            cnslTxt = ConvertToConsoleText(value.Replace("\\n", "\n"));

        }
    }
    
    public string beginText;
    public string endText;
    public static CommandLineManager instance;
    public VerticalLayoutGroup vlg;
    public LayoutGroup lg;
    public float characterCost;
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
        // for (int i = 0; i < col.Count; i++)
        //  {
        //   convertingTextDictionary.Add("[bc:" + (i+1) + "]", "<font=\"wcp 2\"><mark=#"+ColorUtility.ToHtmlStringRGB(col[i]/*+new Color(0.2f, 0.2f, 0.2f)*/)+">");
        // }
      
    }
    public string ConvertToConsoleText(string s)
    {
        string b = s;
        foreach(KeyValuePair<string,string> kvp in convertingTextDictionary)
        {
           b= b.Replace(kvp.Key, kvp.Value);
        }
        return b;
    }
    public string ConvertFromConsoleText(string s)
    {
        string b = s;

        foreach (KeyValuePair<string, string> kvp in convertingTextDictionary)
        {
           b= b.Replace(kvp.Value, kvp.Key);

        }
        return b;
    }
    public IEnumerator Write(string s)
    {
        yield return new WaitUntil(() => Processor.instance.UseRam(10));
       string[] ss= s.Split('\n');
        List<string> a = new List<string>(); 
        for (int i = 0; i < ss.Length; i++)
        {
            string[] currentstr = ss[i].Split(' ');
            for (int j= 0; j < currentstr.Length; j++)
            {
                yield return new WaitUntil(() => Processor.instance.UseRam(Mathf.RoundToInt(currentstr[j].Length * characterCost)));
                consoleText += currentstr[j] + " ";

                foreach (KeyValuePair<string, string> kvp in convertingTextDictionary)
                {


                    if (currentstr[j].Contains(kvp.Key))
                    {
                        a.Add(kvp.Value);
                    }
                }
                UpdateText();

            }
            consoleText += Environment.NewLine;
            UpdateText();
        }
        for (int i = 0; i < a.Count; i++)
        {
            string v;
            if(!convertingTextDictionary.TryGetValue(a[i],out v))
            {
                continue;
            }
            if (v.Contains("color"))
            {
                consoleText += "</color>";
            }
            else if (v.Contains("mark"))
            {
                consoleText += "</mark></font>";
            }
            else
            {
               
            }
          

        }

        UpdateText();



    }

    public void UpdateText()
    {
        TMP.text = beginText.Replace("{fontSize}",(ThemeManager.instance.selectedSize.fontSize * (5f / 6f)).ToString("0")) + cnslTxt + endText;

    }
  
   
   
    IEnumerator fixer()
    {
        yield return new WaitForSeconds(0.2f);
        Write("");
    }

}
[Serializable]
public class FontSizeSettings
{
    public int lineCharSize=1;
    public int fontSize = 1;
}
