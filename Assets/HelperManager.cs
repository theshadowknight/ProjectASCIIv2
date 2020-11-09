using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperManager : MonoBehaviour
{
    public static HelperManager instance;
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
    public string SizeConventer(long l)
    {
        if (l > 1073741824)
        {
            return (l / 1073741824).ToString("0") + "GB";
        }else if (l > 1048576)
        {
            return (l / 1048576).ToString("0") + "MB";
        }else if (l > 1024)
        {
            return (l / 1024).ToString("0") + "KB";
        }
        return l.ToString("0") + "B";

        
    } 
}
