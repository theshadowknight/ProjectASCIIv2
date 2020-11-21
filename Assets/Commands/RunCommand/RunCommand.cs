using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Run Command", menuName = "Commands/Run Command")]

public class RunCommand : BaseCommand
{
    bool broker = false;
    public override IEnumerator Execute(string[] args)
    {
        
        if (!broker)
        {
            if (args.Length == 1)//find default file
            {
                
                yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(args[0]));
                File f = StorageMemoryManager.instance.bufferFileGet;
                Variable v = StorageMemoryManager.instance.registryVariables.Find(x => x.name ==  f.extension+"_executor");
               
                broker = true;
                
                yield return Execute(new string[] { v.data, args[0] });
            }
        }
        if (args.Length == 2)
        {
            if (args[0] == "noter.exe")
            {
                yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(args[1]));
                NoterLogic.instance.currentFile = StorageMemoryManager.instance.bufferFileGet;
                NoterLogic.instance.Load();
                EffectManager.instance.TransferToTS();
                commandOutput = new Variable("out", VariableType.String, "exiting Cosnole... ");

                yield break;
            }
        }

        broker = false;
        yield break;
    }
}
