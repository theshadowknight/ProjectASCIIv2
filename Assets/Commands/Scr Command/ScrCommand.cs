using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "Scr Command", menuName = "Commands/Scr Command")]

public class ScrCommand : BaseCommand
{
    public override IEnumerator Execute(string[] args)
    {

        //scr -m path name extension data
        //scr -m -c name extension data
        //scr -d -c
        //scr -d path
        List<string> argsList = args.ToList();
        if (argsList.Contains("-cl"))
        {
            CommandLineManager.instance.cnslTxt = "";
            commandOutput = new Variable("out", VariableType.String, "");
            yield break;
        }

        yield break;
    }  
 }
    
