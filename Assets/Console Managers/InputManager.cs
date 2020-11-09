using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        if (inputField.text == null || inputField.text == string.Empty || inputField.text == "")
        {
            inputField.ActivateInputField();

            return;

        }
        TextScreenManager.instance.Write(StorageMemoryManager.instance.GetCurrentPath()+">"+ inputField.text);
        string[] inputSplit = inputField.text.Split(' ');
        string commandName = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();
        if (CommandManager.instance.IsCommand(commandName))
        {
            Variable v = CommandManager.instance.ExecuteCommand(commandName, (string[])args);
            if (v.type == VariableType.Error)
            {
                TextScreenManager.instance.Write(""+v.data);
            }
            else
            {
                TextScreenManager.instance.Write(v.data);
            }
        }
        else
        {
            TextScreenManager.instance.Write("Can't find entered command! Try using 'help' command.\n");
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
