using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dir Command", menuName = "Commands/Dir Command")]

public class DirCommand : BaseCommand
{
   /* public override IEnumerator Execute(string[] args,out Variable output)
    {

        //dir path
       //dir -m filepath path
       


        if (args.Length == 0)
        {
            output= new Variable("out", VariableType.String, DisplayAt( StorageMemoryManager.instance.GetCurrentPath()));
        }
        else if (args.Length == 2)
        {
            if (args[0] == "-g")
            {
                File f = StorageMemoryManager.instance.GetFileFormPath(args[1]);
                if (f == null)
                {
                    output = new Variable("error", VariableType.Error, "Wrong path! No file found at " + args[1] + "!");

                }
                StorageMemoryManager.instance.currentFile = f;
                StorageMemoryManager.instance.ChangePathText();
                output = new Variable("out", VariableType.Bool, (StorageMemoryManager.instance.currentFile == f).ToString());
            }else if (args[0] == "-s")
            {
                output = new Variable("out", VariableType.String, DisplayAt(StorageMemoryManager.instance.Pather( args[1])));

            }
            else
            {
                output = new Variable("error", VariableType.Error, "Wrong tag! Use '-g' or '-s'.");

            }
        }
        else if (args.Length > 2)
        {
            if (args[0] == "-m")
            {
                File f = StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather( args[1]));
                File f2 = StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.Pather(args[2]));
                if (f == null)
                {
                    output = new Variable("error", VariableType.Error, "Wrong path! No file found at" + args[0] + "!");
                }
                if (f2 == null)
                {
                    output = new Variable("error", VariableType.Error, "Wrong path! No file found at" + args[1] + "!");
                }
                // StorageMemoryManager.instance.currentFile = f;
                // StorageMemoryManager.instance.ChangePathText();
                File f3 = f.parentFile;
                f2.files.Add(f);
                f3.files.Remove(f);
                f2.SetParentToChildren();
                output = new Variable("out", VariableType.Bool, ((f2.files.Contains(f)) && (!f3.files.Contains(f))).ToString());
            }
        }
        Processor.instance.AddToProcessor(new ProcessorTask(10,1,()=> { }));
        output = base.Execute(args);
    }
   */
    public string DisplayAt(string path)
    {
      
        File f = StorageMemoryManager.instance.GetFileFormPath(path);
        string s = f.GetPath()+"/"+f.GetFullName() + ":\n";
        if (f == null)
        {
            s += "No file found!";
            return s;
        }
        s += string.Format("{0,3}: {1,16} {2,4} {3,6}\n", "", "Full name", "Ext.", "Size");
        for (int i = 0; i < f.files.Count; i++)
        {
            s += string.Format("{0,3}: {1,16} {2,4} {3,6}\n",i, f.files[i].GetFullName(), f.files[i].extension, HelperManager.instance.SizeConventer(f.files[i].GetSize()));
        }
        return s;
    }
}
