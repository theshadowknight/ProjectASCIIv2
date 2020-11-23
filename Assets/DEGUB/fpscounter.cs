using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperFunctions;
public class fpscounter : MonoBehaviour
{
    public Segment7Block s0;

    public Segment7Block s1;
    public Segment7Block s2;
    public Segment7Block s3;

    public void Start()
    {
        StartCoroutine(step());
        StopCoroutine(step());
    }
    IEnumerator step()
    {
        while (true)
        {
            string s = (1f / Time.unscaledDeltaTime).ToString("0");
            if (s.Length > 3)
            {
                s0.Set(Functions.segement[int.Parse(s[0].ToString())]);
                s1.Set(Functions.segement[int.Parse(s[1].ToString())]);
                s2.Set(Functions.segement[int.Parse(s[2].ToString())]);
                s3.Set(Functions.segement[int.Parse(s[3].ToString())]);

            }
            else if (s.Length > 2)
            {
                s0.Set("0000000");

                s1.Set(Functions.segement[int.Parse(s[0].ToString())]);
                s2.Set(Functions.segement[int.Parse(s[1].ToString())]);
                s3.Set(Functions.segement[int.Parse(s[2].ToString())]);

            }
            else if (s.Length > 1)
            {
                s0.Set("0000000");

                s1.Set("0000000");
                s2.Set(Functions.segement[int.Parse(s[0].ToString())]);
                s3.Set(Functions.segement[int.Parse(s[1].ToString())]);
            }
            else
            {
                s0.Set("0000000");

                s1.Set("0000000");
                s2.Set("0000000");
                s3.Set(Functions.segement[int.Parse(s[0].ToString())]);
            }

            yield return new WaitForSeconds(0.5f);

        }
    }
   
}
