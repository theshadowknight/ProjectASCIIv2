using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextScreenManager TSM;
    public List<string> lastCommands = new List<string>();
    public int maxLastCommand = 1;
    public int currentLastCommand = 0;
    public static InputManager instance;
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
    public void Start()
    {
        inputField.ActivateInputField();
    }
    public void Done()
    {
        string[] inputSplit = inputField.text.Split(' ');
        string commandName = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();
        if (CommandManager.instance.IsCommand(commandName))
        {
            CommandManager.instance.ExecuteCommand(commandName, (string[])args);
        }
        else
        {
            TextScreenManager.instance.Write("Can't find command \"" + commandName + "\" with arguments \"" + (string)string.Join(" ", args) + "\"! Try using \"help\" command.\n");
        }
        if (inputField.text != null && inputField.text != string.Empty && inputField.text != "")
        {
            lastCommands.Add(inputField.text);
        }
        inputField.text = string.Empty;
        inputField.ActivateInputField();
        currentLastCommand = 0;

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentLastCommand < maxLastCommand && lastCommands.Count - 2 - currentLastCommand > 0)
            {
                currentLastCommand++;
            }
            inputField.text = lastCommands[lastCommands.Count - 1 - currentLastCommand];
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentLastCommand > 0)
            {
                currentLastCommand--;
            }
            inputField.text = lastCommands[lastCommands.Count - 1 - currentLastCommand];
        }

    }
}
