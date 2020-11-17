using System.Collections;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Test Command", menuName = "Commands/Test Command")]
public class TestCommand : BaseCommand
{



    public override IEnumerator Execute(string[] args)
    {
        /*  System.Random r = new System.Random();
          string l = "";
              l+= ((char)0);
          string joined = String.Join(l, args);
          string summedOutput = "";
          for (int i = 0; i < args.Length; i++)
          {
             //h= args[i].Replace("\\n", "\n");


          }
          int jPosition = 0;

          for (int i = 0; i < 30; i++)
          {
              string bu = "";
              if (joined.Length <= jPosition)
              {
                  break;
              }
              for (int j = 0; j < (ThemeManager.instance.selectedSize.lineCharSize-1) / 4; j++)
              {
                  if (joined.Length <= jPosition)
                  {
                      summedOutput += "00 ";
                      bu += ".";
                      jPosition++;
                  }
                  else
                  {

                      summedOutput += string.Format("{0,2:X2} ", (int)joined[jPosition]);
                      if (joined[jPosition] == 0)
                      {
                          bu += ".";
                      }
                      else
                      {
                          bu += joined[jPosition];
                      }
                      jPosition++;
                  }
              }
              summedOutput += " " + bu;
              summedOutput += '\n';
          }
          */
        if (args[0] == "noter")
        {

        }
        commandOutput = new Variable("out", VariableType.String, "");


        yield break;
    }

}
