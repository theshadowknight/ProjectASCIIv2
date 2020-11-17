using System.Collections;
using UnityEngine;
using System.Collections.Generic;

// [CreateAssetMenu(fileName = "Base Command", menuName = "Commands/Base Command")]

public abstract class BaseCommand : ScriptableObject
{
    public string commandWord;
    public string quickHelp;
    [Multiline(6)]
    public string helpText;
    [HideInInspector]
    public Variable commandOutput;
   
    public virtual IEnumerator Execute(string[] args)
    {
        yield return new WaitUntil(()=>Processor.instance.UseRam(10));
        commandOutput = new Variable("out", VariableType.Bool, "false"); 
    }
}
