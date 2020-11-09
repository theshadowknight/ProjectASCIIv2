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

    public void S()
    {
      
    }
    public virtual IEnumerator Execute(string[] args)
    {
       // output = new Variable("out",VariableType.Bool,"false");
        yield return new WaitUntil(()=>Processor.instance.UseRam(100));
        yield return new Variable("out", VariableType.Bool, "false"); 
    }
}
