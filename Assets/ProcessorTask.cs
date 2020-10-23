using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class ProcessorTask 
{
    public int RAMusage;
    public int tactsToEnd;
    public int tactsLeft;
    public delegate void PTFunction();
    public PTFunction PTfunction;
    public ProcessorTask(int RAMu, int tte, PTFunction d)
    {
        RAMusage = RAMu;
        tactsToEnd = tte;
        tactsLeft = tte;
        PTfunction = d;
    }
}
