using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tester : MonoBehaviour
{
    public TextMeshProUGUI t;
    public string c;
    public string cprep;
    public List<string> strings = new List<string> { "The Pain", "is too much for", "me", "i..", "can't", "do.", "..this.", "much longer" };
    public List<char> chars = new List<char>();
    public void Awake()
    {
       // c = t.text;
       // cprep = c.Replace(strings[0], "00").Replace(strings[1], "01").Replace(strings[2], "02").Replace(strings[3], "03").Replace(strings[4], "04").Replace(strings[5], "05").Replace(strings[6], "06").Replace(strings[7], "07").Replace("\\n", "08").Replace(Environment.NewLine, "08");
       // StartCoroutine("ch");
    }
    IEnumerator ch()
    {
        while (true)
        {
            string n = "";
            foreach (char c in cprep.ToCharArray())
            {
                if (c == '.')
                {
                    n += chars[UnityEngine.Random.Range(0, chars.Count)];
                    continue;
                }
                if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8')
                {
                    n += c;

                }
                else
                {
                    n += c;
                   // n += chars[UnityEngine.Random.Range(0, chars.Count)];
                }
            }
            n = n.Replace("00", strings[0]).Replace("01", strings[1]).Replace("02", strings[2]).Replace("03", strings[3]).Replace("04", strings[4]).Replace("05", strings[5]).Replace("06", strings[6]).Replace("07", strings[7]).Replace("08", Environment.NewLine);

            t.text = "<mspace=18><line-height=30>\n" + n + "\n</line-height></mspace>";
            yield return new WaitForSeconds(0.1f);
        }
    }
  
}
