using System;
using System.Collections.Generic;
using System.Linq;
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

   
   
   

    public void test()
    {
        Debug.LogError("test");
    }
}
