using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextScreenManager : MonoBehaviour
{
    public static TextScreenManager instance;
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
    public TextMeshProUGUI screenText;
    //[Multiline(5)]
    public List<line> baseScreen;
   // [Multiline(5)]

    public List<string> screen;
    public int Ypos;
    public int Xpos;
    public string inputStr="";
    bool b;
    public void Start()
    {
        //baseScreen[34].str = string.Format("[bc:15][c:16]CTR+C - Quit{0,56}[c:0][bc:0]",".");
      //  baseScreen[33].str = string.Format("[bc:15][c:16]{0,68}[c:0][bc:0]", ".");


        Ypos = 2;
        Display();
        StartCoroutine(C());
    }
    public IEnumerator C()
    {
        while (true)
        {
            
           
            b = !b;
            Display();
            yield return new WaitForSeconds(0.4f); 
        }
    }
    public void TryMoveY(int delta)
    {
       if(baseScreen[ Ypos + delta].replace)
        {

        }
        else
        {
            Ypos += delta;
            Xpos = screen[Ypos].Length;
            inputStr = screen[Ypos];
            Display();
        }
    }
    public void TryMoveX(int delta)
    {
        if (screen[Ypos].Length>Xpos+delta)
        {
            Xpos += delta;
        }
        else 
        {
            Xpos = screen[Ypos].Length;

        }
        Display();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryMoveY(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryMoveY(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryMoveX(-1);

        }
    
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryMoveX(1);
        }
    
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (inputStr.Length != 0)
                {
                    inputStr =/* inputStr.Substring(0, inputStr.Length - 1);*/ inputStr.Substring(0, Xpos-1) +  inputStr.Substring(Xpos);
                    TryMoveX(-1);
                }
                else
                {
                    //TryMoveY(-1);
                    screen.RemoveAt(Ypos);
                    screen.Add("");
                    Display();
                    return;
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                string s = screen[Ypos + 1] + inputStr.Substring(Xpos);

                inputStr = inputStr.Substring(0, Xpos );
                screen[Ypos] = inputStr;
                TryMoveY(1);
                screen[Ypos] = s;
                inputStr = screen[Ypos];
            }
            else
            {
                if (inputStr.Length < 1)
                {
                    inputStr += c;
                    Xpos++;
                }
                else
                {
                    inputStr = inputStr.Substring(0, Xpos) + c + inputStr.Substring(Xpos);
                    Xpos++;
                }
            }
            screen[Ypos] = inputStr;
            Display();
        }
       
       
    }
    /* public string replace()
     {
         string[] str = screen.Split('\n');
         for (int i = 0; i < str.Length; i++)
         {
          //   str.
         }
         //screen.Substring(0, pos) + inputStr + screen.Substring(pos + inputStr.Length);
         return "";
     }*/
    public void Display()
    {
        string str = "";
        for (int i = 0; i < baseScreen.Count; i++)
        {
          
            if (b)
            {
                if (!baseScreen[i].replace)
                {
                    str += string.Format("{0,3} ", (i - 2).ToString("0"));
                    if (screen.Count <= i)
                    {
                        str += "\n";
                    

                    }
                    else
                    {
                        str += screen[i] + "\n";
                    }
                }
                else
                {
                    str += baseScreen[i].str + "\n";
                }
            }
            else
            {

                if (!baseScreen[i].replace)
                {
                    str +=string.Format("{0,3} ", (i - 2).ToString("0"));
                    if (screen.Count <= i)
                    {
                        string h = "";
                        if (i == Ypos)
                        {
                             h = screen[Ypos].Substring(Xpos) + "[bc:15][c:16]" + "_" + "[c:0][bc:0]" ;
                        }
                        str += h+"\n";
                    }
                    else
                    {
                        string h = "";
                        if (i == Ypos)
                        {
                            if (screen[Ypos].Length> Xpos)
                            {
                                str +=  screen[Ypos].Substring(0,Xpos) + "[bc:15][c:16]" + screen[Ypos][Xpos] + "[c:0][bc:0]" + screen[Ypos].Substring(Xpos+1, screen[Ypos].Length - Xpos-1) + "\n";
                            }
                            else
                            {
                                str +=  screen[Ypos].Substring(0,Xpos) + "[bc:15][c:16]" + "_" + "[c:0][bc:0]" + "\n";

                            }
                        }
                        else
                        {
                            str += screen[i] + "\n";
                        }
                      
                    }
                }
                else
                {
                    str += baseScreen[i].str + "\n";
                }

            }
        }
        screenText.text = "<mspace=17>" + CommandLineManager.instance.ConvertToConsoleText(str);
    }
    [System.Serializable]
    public class line
    {
        public string str="";
        public bool replace=false;
    }
}
