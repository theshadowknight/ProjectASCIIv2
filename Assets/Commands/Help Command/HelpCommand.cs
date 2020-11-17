using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Help Command", menuName = "Commands/Help Command")]

public class HelpCommand : BaseCommand
{
    public override IEnumerator Execute(string[] args)
    {
        //TextScreenManager.instance.Write(helpText);
        if (args.Length < 1)
        {
            string s = helpText + '\n';

            for (int i = 0; i < CommandManager.instance.commands.Count; i++)
            {
                s += string.Format(" {0,5} - " + CommandManager.instance.commands[i].quickHelp + " \n", CommandManager.instance.commands[i].commandWord);
            }
            s += "Use 'help <command>' for more help\n";
            commandOutput = new Variable("out", VariableType.String, s);
        }
        else if (args.Length < 2)
        {
         BaseCommand bc=   CommandManager.instance.commands.Find(x => x.commandWord == args[0]);
            if (bc == null)
            {
                commandOutput = new Variable("error", VariableType.NULL, "Can't find command with name '"+args[0]+"'!");
                        yield break;

            }
            string s = "Help for command: " + bc.commandWord+"\n";
            s +="Quick help:"+ bc.quickHelp + "\n";

            s += bc.helpText + "\n";
            commandOutput = new Variable("out", VariableType.String, s);

        }

        yield break;
    }
}
