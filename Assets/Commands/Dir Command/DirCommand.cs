using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperFunctions;
using System;

[CreateAssetMenu(fileName = "Dir Command", menuName = "Commands/Dir Command")]

public class DirCommand : BaseCommand
{
    private string displayOutput;
    public override IEnumerator Execute(string[] args)
    {

        //dir path
       //dir -m filepath path
       


        if (args.Length == 0)
        {
            ConnectionTypes t = ConnectionTypes.User;
            int a = Functions.EnumFromRegistryVariables(typeof(ConnectionTypes), "current_user");
            if (a != -1)
            {
                t = (ConnectionTypes)a;
            }

            yield return DisplayAt(StorageMemoryManager.instance.GetCurrentPath(),t);
            yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(StorageMemoryManager.instance.GetCurrentPath()));
            File f = StorageMemoryManager.instance.bufferFileGet;
            string ext = f.extension;
            if (ext == "")
            {
                ext = "DIR";
            }
            displayOutput = string.Format("Main:{1,15} {2,4} {3,6} {4,9}\n", 0, f.GetFullName(), ext, Functions.SizeConventer(f.GetSize()), f.GetPremissionStr()) + displayOutput;

            commandOutput = new Variable("out", VariableType.String,displayOutput);
            yield return new WaitUntil(() => Processor.instance.UseRam(10));

            yield break;
        }
        else if (args.Length == 2)
        {
            if (args[0] == "-g")
            {
                yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(args[1]));
                File f = StorageMemoryManager.instance.bufferFileGet;
                  
                if (f == null)
                {
                    commandOutput = new Variable("error", VariableType.NULL, "Wrong path! No file found at " + StorageMemoryManager.instance.Pather(args[1]) + "!");
                    yield break;

                }
                StorageMemoryManager.instance.currentFile = f;
                StorageMemoryManager.instance.ChangePathText();
                commandOutput = new Variable("out", VariableType.Bool, (StorageMemoryManager.instance.currentFile == f).ToString());
                yield break;

            }
            else if (args[0] == "-s")
            {
                ConnectionTypes t = ConnectionTypes.User;
                int a = Functions.EnumFromRegistryVariables(typeof(ConnectionTypes), "current_user");
                if (a!=-1)
                {
                    t = (ConnectionTypes)a;
                }
               yield return DisplayAt(StorageMemoryManager.instance.Pather(args[1]),t);
                yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(args[1]));
                File f = StorageMemoryManager.instance.bufferFileGet;
                string ext = f.extension;
                if (ext == "")
                {
                    ext = "DIR";
                }
                displayOutput = string.Format("Main: {1,16} {2,4} {3,6} {4,9}\n", 0, f.GetFullName(), ext, Functions.SizeConventer(f.GetSize()), f.GetPremissionStr())+ displayOutput;

                commandOutput = new Variable("out", VariableType.String,displayOutput);
                yield break;

            }
            else
            {
                commandOutput = new Variable("error", VariableType.NULL, "Wrong tag! Use '-g' or '-s'.");
                yield break;

            }
        }
        else if (args.Length > 2)
        {
            if (args[0] == "-t")
            {
                yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(args[1]));
                File f = StorageMemoryManager.instance.bufferFileGet;
                yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(args[2]));

                File f2 = StorageMemoryManager.instance.bufferFileGet;
                if (f == null)
                {
                    commandOutput = new Variable("error", VariableType.NULL, "Wrong path! No file found at" + args[0] + "!");
                    yield break;

                }
                if (f2 == null)
                {
                    commandOutput = new Variable("error", VariableType.NULL, "Wrong path! No file found at" + args[1] + "!");
                    yield break;

                }
                // StorageMemoryManager.instance.currentFile = f;
                // StorageMemoryManager.instance.ChangePathText();
                File f3 = f.parentFile;
                f2.files.Add(f);
                f3.files.Remove(f);
                f2.SetParentToChildren();
                commandOutput = new Variable("out", VariableType.Bool, ((f2.files.Contains(f)) && (!f3.files.Contains(f))).ToString());
                yield break;

            }
        }
       // Processor.instance.AddToProcessor(new ProcessorTask(10,1,()=> { }));
        commandOutput = new Variable("out",VariableType.NULL,"NULL");
    }
   
    public IEnumerator DisplayAt(string path,ConnectionTypes ct)
    {
        yield return new WaitUntil(() => Processor.instance.UseRam(10));

        yield return StorageMemoryManager.instance.GetFileFormPath(path);
        File f = StorageMemoryManager.instance.bufferFileGet;
      string s = f.GetPath()+"/"+f.GetFullName() + ":\n";
        if (f == null)
        {
            s += "No file found!";
            yield return new WaitUntil(() => Processor.instance.UseRam(40));

            displayOutput = s;
            yield break;
        }
        yield return new WaitUntil(() => Processor.instance.UseRam(10));

        s += string.Format("{0,3} {1,16} {2,4} {3,6} {4,9}\n", "Num", "Full name", " Ext", "Size","Premiss.");
        for (int i = 0; i < f.files.Count; i++)
        {
            if (!f.files[i].GetPremission(ct, PremissionTypes.Read))
            {
                continue;
            }
            yield return new WaitUntil(() => Processor.instance.UseRam(40));
            string ext = f.files[i].extension;
            if (ext == "")
            {
                ext = "DIR";
            }
            
            s += string.Format("{0,2}: {1,16} {2,4} {3,6} {4,9}\n", i, f.files[i].GetFullName(), ext, Functions.SizeConventer(f.files[i].GetSize()),f.files[i].GetPremissionStr());
        }
        yield return new WaitUntil(() => Processor.instance.UseRam(100));

        displayOutput = s;

        yield break;

    }
}
