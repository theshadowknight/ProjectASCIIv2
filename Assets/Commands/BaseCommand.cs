using System.Collections;
using UnityEngine;
// [CreateAssetMenu(fileName = "Base Command", menuName = "Commands/Base Command")]

public abstract class BaseCommand : ScriptableObject
{
    public string commandWord;
    public string quickHelp;
    [Multiline(6)]
    public string helpText;


    public virtual IEnumerator Execute(string[] args,ref Variable output)
    {
        output = new Variable("out",VariableType.Bool,"false");
        yield return new WaitUntil(Processor.instance.UseRam(100));
    }
}
