using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Segment7Block : MonoBehaviour
{
    public Image top;//1
    public Image mid;//7
    public Image bot;//4

    public Image tl;//6
    public Image tr;//2

    public Image bl;//5
    public Image br;//3

    public string s;
    public Color on;
    public Color off;
    public void Set(string str)
    {
        SetSegment(top, str[0] == '1');
        SetSegment(tr, str[1] == '1');
        SetSegment(br, str[2] == '1');

        SetSegment(bot, str[3] == '1');
        SetSegment(bl, str[4] == '1');
        SetSegment(tl, str[5] == '1');

        SetSegment(mid, str[6] == '1');

    }
    public void SetSegment(Image segment, bool o)
    {
        segment.color = o ? on : off;
    }
    public void Start()
    {

       // StartCoroutine(setter());
    }
    public IEnumerator setter() {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Set(s);
         //   yield return new WaitForSeconds(1f / 120f);
          //  Set("0000000");
        }
    }
}
