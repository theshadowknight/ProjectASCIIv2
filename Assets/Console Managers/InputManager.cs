using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public CommandLineManager TSM;
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
    public void OnEnable()
    {
        inputField.ActivateInputField();
    }
    public void Done()
    {
        StartCoroutine(ParseInput());
    }
    public IEnumerator ParseInput()
    {

        // inputField.interactable = false;
        if (inputField.text == null || inputField.text == string.Empty || inputField.text == "")
        {
            inputField.ActivateInputField();
            //  inputField.interactable = true;

            yield break;

        }
        yield return CommandLineManager.instance.Write(StorageMemoryManager.instance.GetCurrentPath() + ">" + inputField.text);
        string[] inputSplit = inputField.text.Split(' ');
        string commandName = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();
        if (CommandManager.instance.IsCommand(commandName))
        {
            yield return CommandManager.instance.ExecuteCommand(commandName, (string[])args);
            Variable v = CommandManager.instance.commandOutput;
            if (v.data == "exiting Cosnole... ")
            {
                lastCommands.Add(inputField.text);
                currentLastCommand = 0;
                yield return CommandLineManager.instance.Write(v.data);
                inputField.text = string.Empty;
                yield break;
            }
            if (v.type == VariableType.NULL)
            {
                yield return CommandLineManager.instance.Write("[c:12]" + v.data + "[c:0]");
            }
            else
            {
                yield return CommandLineManager.instance.Write(v.data);
            }
        }
        else
        {
            yield return CommandLineManager.instance.Write("Can't find entered command! Try using 'help' command.\n");
        }
        if (inputField.text != null && inputField.text != string.Empty && inputField.text != "")
        {
            lastCommands.Add(inputField.text);
        }
        inputField.text = string.Empty;
        inputField.ActivateInputField();
        currentLastCommand = 0;
        // inputField.interactable = true;

        yield break;
    }
    public void CheckIfItIsCorrect()
    {

    }
    public void ResetInput()
    {
        inputField.text = StorageMemoryManager.instance.gett();
        inputField.stringPosition = StorageMemoryManager.instance.gett().Length;
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
