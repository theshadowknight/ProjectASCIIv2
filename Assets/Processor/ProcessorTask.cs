﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class ProcessorTask 
{
    public int RAMgiven=0;
    public int RAMneeded;
    public int tactsToEnd;
    public int tactsLeft;
    public delegate void PTFunction();
    public PTFunction PTfunction;
    public ProcessorTask(int RAMu, int tte, PTFunction d)
    {
        RAMneeded = RAMu;
        tactsToEnd = tte;
        tactsLeft = tte;
        PTfunction = d;
    }
}
