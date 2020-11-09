using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Processor : MonoBehaviour
{
    public int RAMmaxSize;
    public int RAMcurrSize;

    public int tactsPS;
    private float tactWaitTime;
    public int maxTackLoops;
    public List<ProcessorTask> PTaskStack = new List<ProcessorTask>();
    public static Processor instance;
    public IEnumerator mainTacter;
  
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
    public void UpdateTwt()
    {
        tactWaitTime = 1.0f / tactsPS;
    }
    IEnumerator tact()
    {
        while (true)
        {
            DoTasks();
            Debug.Log("tacted");
            yield return new WaitForSeconds(tactWaitTime);
        }
    }
    public void AddToProcessor(ProcessorTask pt)
    {
        PTaskStack.Add(pt);

    }
    public void DoTasks()
    {
        RAMcurrSize = RAMmaxSize;
        int tactLoops = 0;
        while (RAMcurrSize > 0 && PTaskStack.Count>0)
        {
            if (tactLoops > maxTackLoops)
            {
                return;
            }
            for (int i = 0; i < PTaskStack.Count; i++)
            {
                if ((PTaskStack[i].RAMneeded- PTaskStack[i].RAMgiven) <= RAMcurrSize)
                {
                    RAMcurrSize -= (PTaskStack[i].RAMneeded - PTaskStack[i].RAMgiven);
                    PTaskStack[i].tactsLeft--;
                    if (PTaskStack[i].tactsLeft < 1)
                    {
                        PTaskStack[i].PTfunction.Invoke();
                        PTaskStack.RemoveAt(i);
                        i--;
                    }
                }
                else
                {
                    PTaskStack[i].RAMgiven += RAMcurrSize;
                    RAMcurrSize = 0;
                }

            }
            RAMcurrSize--;
            tactLoops++;
        }
    }
    public void Start()
    {
        mainTacter = tact();
        UpdateTwt();
        StartCoroutine(mainTacter);
    }
  public bool UseRam(int amount)
    {
        if (RAMcurrSize >= amount)
        {
            RAMcurrSize -= amount;
            return true;
        }
            return false;
    }
}
