using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Help Command", menuName = "Commands/Help Command")]

public class HelpCommand : BaseCommand
{
    public override Variable Execute(string[] args)
    {
        //TextScreenManager.instance.Write(helpText);
        string s = helpText + '\n';
       
        for (int i = 0; i < CommandManager.instance.commands.Count; i++)
        {
            s += string.Format("{0,9} -{1,35} {2,15} help' to get more info.\n", CommandManager.instance.commands[i].commandWord, CommandManager.instance.commands[i].quickHelp + ". Use", "'" + CommandManager.instance.commands[i].commandWord + " help'");// CommandManager.instance.commands[i].commandWord+" - "+ CommandManager.instance.commands[i].quickHelp+"\t .Use '" + CommandManager.instance.commands[i].commandWord+ " help' to get more info." + '\n';
        }
        return new Variable("out",VariableType.String,s);
    }
}
