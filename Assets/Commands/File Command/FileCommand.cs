using HelperFunctions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "File Command", menuName = "Commands/File Command")]

public class FileCommand : BaseCommand
{
    public override IEnumerator Execute(string[] args)
    {

        //file -m path name extension data
        //file -m -c name extension data
        //file -d -c
        //file -d path
        if (args.Length < 2)
        {
            commandOutput= new Variable("error", VariableType.NULL, "Not enough arguments!");
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
                        commandOutput= new Variable("error", VariableType.NULL, "Not enough arguments!");
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
                    
                    yield return  StorageMemoryManager.instance.FileMakerAtPath(path, namer,ext,data);

                    File f = StorageMemoryManager.instance.bufferFileMade;
                    commandOutput= new Variable("out", VariableType.Bool, (f != null).ToString());
                    StorageMemoryManager.instance.bufferFileMade = null;
                    yield break;
                        }
                case "-d":
                    {
                    yield return StorageMemoryManager.instance.RemoveFileAtPath(path);
                    commandOutput = StorageMemoryManager.instance.removalOutput;

                    yield break;
                }
               
            }

        commandOutput = new Variable("out", VariableType.NULL, "NULL");
        yield break;
    }
}
