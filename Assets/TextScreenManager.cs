using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextScreenManager : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    [Multiline(6)]
    public string consoleText;
    public string beginText;
    public string endText;
    public static TextScreenManager instance;
    public VerticalLayoutGroup vlg;
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

    public void Write(string s)
    {

        s = s.ToString();
        Processor.instance.AddToProcessor(
      new ProcessorTask(10, 1, new ProcessorTask.PTFunction(() =>
      {
          consoleText += s;

          UpdateText();

      }))
      );



        /* consoleText += s;
         UpdateText();*/
    }

    public void UpdateText()
    {
        TMP.text = beginText + consoleText + endText;
        //  vlg.CalculateLayoutInputVertical();

    }
    public void Start()
    {
        StartCoroutine("fixer");
    }
    IEnumerator fixer()
    {
        yield return new WaitForSeconds(0.2f);
        Write("");
        //  UpdateText();
    }

}
