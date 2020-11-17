using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Processor : MonoBehaviour
{
    public int RAMmaxSize;
    public int RAMcurrSize;

    public int tactsPS;
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
   
    IEnumerator tact()
    {
        while (true)
        {
            // DoTasks();
            if (RAMcurrSize < RAMmaxSize) { 
            RAMcurrSize += RAMmaxSize;
        }
            Debug.Log("tacted");

            yield return new WaitForSeconds(1f/ tactsPS);
        }
    }
    /*public void AddToProcessor(ProcessorTask pt)
    {
        PTaskStack.Add(pt);

    }*/
  /*  public void DoTasks()
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
    }*/
    public void Start()
    {
        mainTacter = tact();
       // UpdateTwt();
        StartCoroutine(mainTacter);
    }
    /* public bool UseRam(int amount)
       {
           if (RAMcurrSize >= amount)
           {
               RAMcurrSize -= amount;
               return true;
           }
               return false;
       }*/

    public bool UseRam(int amount)
    {
        if (RAMcurrSize > 0)
        {
            RAMcurrSize -= amount;
            return true;
        }
        return false;
    }
    public IEnumerator mainThreat()
    {
        while (true)
        {
           
      
            yield return test();
            yield return new WaitForSeconds(1f/ tactsPS);
        }
    }
    public IEnumerator test()
    {
        Debug.LogError("current ram:"+RAMcurrSize);
        yield return new WaitUntil(() => Processor.instance.UseRam(10));
        Debug.LogError("used 10 ram");
        Debug.LogError("current ram:" + RAMcurrSize);
        yield return new WaitUntil(() => Processor.instance.UseRam(1000));
        Debug.LogError("used 1000 ram");
        Debug.LogError("current ram:" + RAMcurrSize);



        yield return new WaitUntil(() => Processor.instance.UseRam(10));
        Debug.LogError("used 10 ram");
        Debug.LogError("current ram:" + RAMcurrSize);

    }
}
