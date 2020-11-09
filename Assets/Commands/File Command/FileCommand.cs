using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "File Command", menuName = "Commands/File Command")]

public class FileCommand : BaseCommand
{
   /* public override Variable Execute(string[] args)
    {

        //file -m path name extension data
        //file -m -c name extension data
        //file -d -c
        //file -d path
        if (args.Length < 2)
        {
            return new Variable("error", VariableType.Error, "Not enough arguments!");
        }
        string path = StorageMemoryManager.instance.Pather(args[1]);
       
        
        switch (args[0])
            {
                case "-m":
                    {
                    string namer = "";
                    string ext = "txt";
                    string data = "";
                   
                        if (args.Length < 3)
                        {
                            return new Variable("error", VariableType.Error, "Not enough arguments!");
                        }
                        namer = args[2];
                        if (args.Length > 3)
                        {
                            ext = args[3];
                        }
                        if (args.Length > 4)
                        {
                            data = args[4];
                        }
                   
                  File f=  StorageMemoryManager.instance.MakeFileAtPath(path, namer,ext,data);
                    return new Variable("out", VariableType.Bool, (f != null).ToString());
                        }
                case "-d":
                    {
                    return StorageMemoryManager.instance.RemoveFileAtPath(path);
                   
                    }
               
            }
        
        return base.Execute(args);
    }*/
}
