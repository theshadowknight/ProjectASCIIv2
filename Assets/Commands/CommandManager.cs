using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public List<BaseCommand> commands = new List<BaseCommand>();
    public static CommandManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public BaseCommand GetCommand(string commandName)
    {
        return commands.Find(x => x.commandWord == commandName);
    }
    public Variable RAW_ExecuteCommand(string rawText)
    {
        string[] inputSplit = rawText.Split(' ');
        string commandName = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();
        return ExecuteCommand(commandName, args);

    }
    public Variable ExecuteCommand(string commandName, string[] args)
    {
        BaseCommand bc = GetCommand(commandName);

        return null;// bc.Execute(args);
    }
    public bool IsCommand(string commandName)
    {
        return commands.Find(x => x.commandWord == commandName) == null ? false : true;
    }
    public bool RAW_IsCommand(string rawText)
    {
        string[] inputSplit = rawText.Split(' ');
        string commandName = inputSplit[0];

        return IsCommand(commandName);
    }
}
