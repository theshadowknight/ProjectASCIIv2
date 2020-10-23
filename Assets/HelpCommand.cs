using UnityEngine;
[CreateAssetMenu(fileName = "Help Command", menuName = "Commands/Help Command")]

public class HelpCommand : BaseCommand
{
    public override bool Execute(string[] args)
    {
        TextScreenManager.instance.Write(helpText);
        TextScreenManager.instance.Write("l\nl");

        return true;
    }
}
