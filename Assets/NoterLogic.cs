using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
public class NoterLogic : MonoBehaviour
{
    public static NoterLogic instance;
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

    public bool caretTick;
    public int posX;
    public int posY;
    public int posPageY = 2;
    public static int posPageYoffset = -2; 
    public List<line> baseScreen;
    // [Multiline(5)]

    public List<string> currentScreen;
    [SerializeReference]
    public File currentFile;
    public string copiedText;
    public Coroutine cor;


    public void OnEnable()

    {

        //   baseScreen[1].text = string.Format("[bc:15][c:16]{0,68}[bc:0][c:0]", ".");
        posPageY = 0;
        posY = 0;
        TryMoveY(0);
        TryMoveX(0);

        Display();
        if (cor == null)
        {
            cor = StartCoroutine(CarretTick());
        } // StartCoroutine(t());

    }
    public IEnumerator CarretTick()
    {
        while (true)
        {


            caretTick = !caretTick;
            Display();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public string inputStr = "";

    public void TryMoveY(int delta)
    {

        if (posY + delta < 0)
        {
        }
        else if (posY + delta >= currentScreen.Count)
        {
          //  TryMoveY(posY-(currentScreen.Count - 1));
           
        }
        else
        {
            if (posX == currentScreen[posY].Length)
            {
                posY += delta;
                posX = currentScreen[posY].Length;
            }
            else
            {
                posY += delta;
                posX = Mathf.Clamp(posX, 0, currentScreen[posY].Length);
            }

            inputStr = currentScreen[posY];
            Display();
        }
    }
    public void TryMoveX(int delta)
    {
        if (posX + delta < 0)
        {

            TryMoveY(-1);
            //     posX = currentScreen[posY].Length ;

        }
        else if (currentScreen[posY].Length > posX + delta)
        {
            posX += delta;
        }
        else
        {
            posX = currentScreen[posY].Length;

        }
        Display();
    }
    public string GetScroll(int pos)
    {
        if (currentScreen.Count <2)
        {
            return "[bc:15]" + "█" + "[bc:0]";
        }
        else
        {

            if (1f * (30f + posPageY) * (30f / (currentScreen.Count - 1)) >= pos - 2 && pos -2>= 1f * (posPageY ) * (30f / (currentScreen.Count - 1)))
            {
                return "[bc:15]" + "█" + "[bc:0]";
                // return pos.ToString("0");
            }
            else
            {
                return "" + "░" + "";
                //return " ";

            }
        }

        return "[bc:15][c:16]" + "█" + "[c:0][bc:0]";
        //posPageY* (32f / currentScreen.Count)
    }

    public string MakeDisplay()
    {
        string str = "";
        int len = (ThemeManager.instance.selectedSize.lineCharSize - 6);
        //len *= -1;
        //  Debug.Log("{0,-" + (len).ToString("0") + "}{1,1}\n");

        for (int j = 0; j < baseScreen.Count; j++)
        {

            string caret = "";
            if (caretTick)
            {
                caret = "[bc:15][c:16]" + "_" + "[c:0][bc:0]";
            }
            else
            {
                caret = "" + "_" + "";
            }


            if (!baseScreen[j].isStaticSpace)
            {

                if (currentScreen.Count <= j + posPageY+ posPageYoffset)
                {
                    if (j + posPageY+ posPageYoffset == posY)
                    {
                        str += caret + "\n";
                    }
                    else
                    {

                        str += string.Format("{0,-" + (len+4).ToString("0") + "}{1,1}\n", "", GetScroll(j));
                    }

                }
                else
                {
                    str += string.Format("{0,3} ", (j + posPageY+ posPageYoffset).ToString("0"));

                    if (j + posPageYoffset + posPageY == posY)
                    {
                        string s = GetScroll(j);
                        //  Debug.LogError(currentScreen[posY].Length + " " + posX + " " + (currentScreen[posY].Length - posX - 1));
                        if (currentScreen[posY].Length > posX)
                        {
                            string a = currentScreen[posY].Substring(0, posX) + caret.Replace('_', currentScreen[posY][posX]) + currentScreen[posY].Substring(posX + 1, currentScreen[posY].Length - posX - 1);
                       
                            str += string.Format(a + "{0," + ( len-1 -( a.Length-caret.Length)+s.Length) + "}\n", GetScroll(j));

                            //str += string.Format("{0,-" + (len).ToString("0") + "}{1,1}\n", a, GetScroll(j));

                        }
                        else
                        {
                          
                            string a = currentScreen[posY].Substring(0, posX) + caret;
                            str += string.Format(a + "{0," + (len-1 - (a.Length - caret.Length)+s.Length) + "}\n", s);

                         //   str += string.Format("{0,-" + (len).ToString("0") + "}{1,1}\n", a, GetScroll(j));

                        }
                    }
                    else
                    {
                        string a = currentScreen[j + posPageY+ posPageYoffset];
                        str += string.Format("{0,-" + (len).ToString("0") + "}{1,1}\n", a, GetScroll(j));


                    }

                }
            }
            else
            {
                str += baseScreen[j].text + "\n";
            }

        }
        return str;
    }

    public void Display()
    {
        TextScreenManager.instance.Display(MakeDisplay());
    }
    public string GetText()
    {
        string str = "";
        for (int i = 0; i < currentScreen.Count; i++)
        {
            // string s = currentScreen[i];
            if (i + 1 == currentScreen.Count)
            {
                str += currentScreen[i];
            }
            else
            {
                str += currentScreen[i] + Environment.NewLine;
            }
        }
        return str;
    }
    public void PageMove(int delta)
    {
        if (posPageY + delta < 0)
        {
            // posPageY = -2;
           // 7-1
        }
        else if (posPageY+ 31 + delta > currentScreen.Count)
        {

        }/*else if (posY - posPageY < ThemeManager.instance.selectedSize.lineSize - 6)
        {
            //  posPageY = -2;
        }*/
        else
        { 
            posPageY += delta;
        }
       // posPageY += delta;
       

       
        Display();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Quit();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                CopyLine();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Save();
                Quit();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Paste();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {

                PageMove(-(ThemeManager.instance.selectedSize.lineSize - 6));

            }
            else if (Input.GetKeyDown(KeyCode.PageUp))
            {
                PageMove((ThemeManager.instance.selectedSize.lineSize - 6));

            }
            if (Input.GetKeyDown(KeyCode.Home))
            {

                posX = 0;
                TryMoveX(0);
                posY = 0;
                TryMoveY(0);

            }
            else if (Input.GetKeyDown(KeyCode.End))
            {

             
                posY = currentScreen.Count-1;
                TryMoveY(0);
                posX = currentScreen[posY].Length;
                TryMoveX(0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                TryMoveY(-1);
                //  Debug.LogWarning(posY + posPageY>0);
                
                    PageMove(-1);
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {              
                TryMoveY(1);
                

                    PageMove(1);

                
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TryMoveX(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                TryMoveX(1);
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {

                PageMove(-1);

            }
            else if (Input.GetKeyDown(KeyCode.PageUp))
            {
                PageMove(1);

            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                posX = 0;
                TryMoveX(0);

            }
            else if (Input.GetKeyDown(KeyCode.End))
            {
                posX = currentScreen[posY].Length;
                TryMoveX(0);

            }
        }
        Enter(Input.inputString);



    }
    public void Enter(string strin)
    {
        foreach (char c in strin)
        {


            if (c == '\b') //backspace
            {
                if (inputStr.Length == 0)//remove line when line is empty
                {
                    if (currentScreen.Count > 1)
                    {
                        TryMoveY(-1);
                        currentScreen.RemoveAt(posY + 1);
                        //  currentScreen.Add("");
                        TryMoveY(0);
                        // inputStr = currentScreen[posY];

                        // Display();
                        PageMove(-1);
                        return;
                    }
                }
                else if (posX == 0)//remove line and transfer stuff up
                {
                    string s = currentScreen[posY];
                    currentScreen.RemoveAt(posY);
                    //   currentScreen.Add("");
                    TryMoveY(-1);
                    posX = currentScreen[posY].Length;
                    currentScreen[posY] += s;
                    inputStr = currentScreen[posY];
                    PageMove(-1);
                }
                else if (inputStr.Length != 0) //remove character
                {
                    inputStr = inputStr.Substring(0, posX - 1) + inputStr.Substring(posX);
                    TryMoveX(-1);
                }


            }
            else if ((c == '\n') || (c == '\r')) // enter/return split text and move down
            {
                string s = inputStr.Substring(posX);
                inputStr = inputStr.Substring(0, posX);
                currentScreen[posY] = inputStr;
                currentScreen.Insert(posY + 1, s);
                //   currentScreen.RemoveAt(currentScreen.Count - 1);

                TryMoveY(1);
                //  currentScreen.Insert(posY, s);
                //  currentScreen.RemoveAt(currentScreen.Count - 1);
                // currentScreen[posY] = s;
                inputStr = currentScreen[posY];
                PageMove(1);
                return;
            }
            else
            {
                if (inputStr.Length < 1)//first character
                {
                    inputStr += c;
                    posX++;
                }
                else //any other
                {
                    inputStr = inputStr.Substring(0, posX) + c + inputStr.Substring(posX);
                    posX++;
                }
            }
            if (currentScreen[posY].Length + 7 > ThemeManager.instance.selectedSize.lineCharSize)//when size is too big
            {
                string s = currentScreen[posY + 1] + inputStr.Substring(posX);

                inputStr = inputStr.Substring(0, posX);
                currentScreen[posY] = inputStr;
                TryMoveY(1);
                currentScreen[posY] = s;
                inputStr = currentScreen[posY];
            }
            currentScreen[posY] = inputStr;
            Display();
        }
    }
    public void Load()
    {
        string[] ss = Regex.Split(currentFile.data, "\n"); //currentFile.data.Split('\n');
        currentScreen = new List<string>();
        foreach (string s in ss)
        {

            currentScreen.Add(s.Replace("\n", "").Replace("\\n", "").Replace(Environment.NewLine, "").Replace("" + (char)13, ""));

            posY++;
        }
        //  posY = 2;
    }
    public void Quit()
    {
        EffectManager.instance.TransferToCL();
    }
    public void Save()
    {
        currentFile.data = GetText();
    }
    public void CopyLine()
    {
        copiedText = currentScreen[posY];
    }
    public void Paste()
    {
        Enter(copiedText);
    }
    [System.Serializable]
    public class line
    {
        public string text = "";
        public bool isStaticSpace = false;
    }
}
