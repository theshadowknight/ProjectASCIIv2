using UnityEngine;
[CreateAssetMenu(fileName = "Base Command", menuName = "Commands/Base Command")]

public abstract class BaseCommand : ScriptableObject
{
    public string commandWord;
    [Multiline(6)]
    public string helpText;


    public virtual bool Execute(string[] args)
    {
        return false;
    }
}
