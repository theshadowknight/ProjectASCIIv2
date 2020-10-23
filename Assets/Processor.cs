using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour
{
    public int RAMmaxSize;
    public int RAMcurrSize;

    public int tactsPS;
    private float tactWaitTime;
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
            Debug.Log("tact");
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
        while (RAMcurrSize > 0&& PTaskStack.Count>0)
        {
            for (int i = 0; i < PTaskStack.Count; i++)
            {
                if (PTaskStack[i].RAMusage <= RAMcurrSize)
                {
                    RAMcurrSize -= PTaskStack[i].RAMusage;
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
                    return;
                }

            }
            RAMcurrSize--;
        }
    }
    public void Start()
    {
        mainTacter = tact();
        UpdateTwt();
        StartCoroutine(mainTacter);
    }
}
