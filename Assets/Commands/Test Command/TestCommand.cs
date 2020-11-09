using UnityEngine;
[CreateAssetMenu(fileName = "Test Command", menuName = "Commands/Test Command")]
public class TestCommand : BaseCommand
{



    public override Variable Execute(string[] args)
    {
        /*for (int i = 0; i < args.Length; i++)
        {
            args[i] = args[i].Replace("\\n", "\n");

            TextScreenManager.instance.Write(args[i] + "\n");
        }*/
       
        return new Variable("out",VariableType.Bool,"true");
    }

}
