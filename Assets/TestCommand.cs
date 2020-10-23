using UnityEngine;
[CreateAssetMenu(fileName = "Test Command", menuName = "Commands/Test Command")]
public class TestCommand : BaseCommand
{



    public override bool Execute(string[] args)
    {

        args[0] = args[0].Replace("\\n", "\n");
        //  Processor.instance.AddToProcessor(new ProcessorTask(20,int.Parse(args[0]), new ProcessorTask.PTFunction(() => { TextScreenManager.instance.Write("["+string.Join(" ",args)+"]"); })));
     
        TextScreenManager.instance.Write(args[0] + "\n");
        return false;
    }

}
