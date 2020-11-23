using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ProgrammingLanguageLogic : MonoBehaviour
{
    [Multiline(7)]
    public string text;
    public List<string> lines = new List<string>();
    // Line
    public List<Line> output = new List<Line>();
    public List<char> normalSpilitters = new List<char>();
    public int debugindex;
    public List<Variable> vars = new List<Variable>();
    public string evaltest = "";

    public void ConvertToCompiled()
    {
        output.Clear();
        lines = new List<string>(Regex.Split(text, "\n"));
        foreach (string line in lines)
        {
            output.Add(new Line(new List<string>(CustomSplitter(line))));

        }
    }

    public List<string> CustomSplitter(string s)
    {
        List<string> strs = new List<string>();
        string curr = "";
        bool dq = false;//double qoute
        bool eqspc = false;
        foreach (char c in s)
        {

            if (dq)
            {
                curr += c;

                if (c == '"')
                {
                    if (curr != "")
                    {
                        strs.Add(curr);

                    }
                    curr = "";
                    dq = false;
                }
                continue;
            }
            if (eqspc)
            {
                if (c == '=')
                {
                    curr += c;
                    if (curr != "")
                    {
                        strs.Add(curr);
                    }
                    curr = "";

                }
                else
                {
                    if (curr != "")
                    {
                        strs.Add(curr);
                    }
                    curr = ""+c;
                }
                eqspc = false;
                continue;
            }
            if (c == '>' || c == '!' || c == '<' || c == '=')
            {
                if (curr != "")
                {
                    strs.Add(curr);
                }
                curr = "";
                eqspc = true;
                curr += c;
            }
            else if (c == '\n' || c == ' ')
            {
                if (curr != "")
                {
                    strs.Add(curr);

                }
                curr = "";
            }
            else if (c == '"')
            {

                if (curr != "")
                {
                    strs.Add(curr);

                }
                curr = "";
                curr += c;

                dq = true;
            }
            else if (normalSpilitters.Contains(c))
            {
                if (curr != "")
                {
                    strs.Add(curr);

                }
                curr = "";
                curr += c;
                if (curr != "")
                {
                    strs.Add(curr);

                }
                curr = "";
            }
            else
            {
                curr += c;
            }
        }
        if (curr != "")
        {
            strs.Add(curr);

        }
        return strs;
    }
    public int clac = 0;
    public int calcmax = 100;
    public string Calculate(List<string> values)
    {
        clac++;
        if (clac > calcmax)
        {
            Debug.LogError("clac; masc");
            return "";
        }
        Debug.Log("calc: " + string.Join("|", values));
        List<string> parts = values;
        int cbstartindex = -1;
        if (values.Contains("("))
        {
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i] == "(")
                {
                    cbstartindex = i;

                }
                if (parts[i] == ")")
                {
                    if (cbstartindex != -1)
                    {

                        string s = Calculate(parts.GetRange(cbstartindex + 1, i - cbstartindex - 1));
                        parts.RemoveRange(cbstartindex, i - cbstartindex+1);
                        parts.Insert(cbstartindex, s);
                        cbstartindex = -1;
                    }
                }
            }
        }
        else
        {
            if (parts.Count < 3)
            {
                if (parts.Count == 1)
                {
                    return parts[0];
                   

                }
                if (parts[0] == "!")
                {
                    if (parts[1] == "false")
                    {
                        return "true";
                    }
                    else
                    {
                        return "false";
                    }
                    return string.Join(" ", parts);
                }
                else
                {
                    return "????";
                }
               
            }
            return Comparer(parts[0], parts[2], parts[1])?"true":"false";
        }
        return Calculate( parts);
    }
    public bool Comparer(string s1, string s2, string compater)
    {
        CompareType ct = CompareType.IsLessThan;
        switch (compater)
        {
            case ">=":
                {
                    ct = CompareType.IsGreaterOrEqualThan;
                    break;
                }
            case ">":
                {
                    ct = CompareType.IsGreaterThan;
                    break;
                }
            case "<":
                {
                    ct = CompareType.IsLessThan;
                    break;
                }
            case "<=":
                {
                    ct = CompareType.IsLessOrEqualThan;
                    break;
                }
            case "==":
                {
                    ct = CompareType.IsEqualTo;
                    break;
                }
            case "!=":
                {
                    ct = CompareType.IsDiffrent;
                    break;
                }
            default:

                {
                    return false;
                }
        }
        return Evaler(s1).AutoCompare(ct, Evaler(s2));
    }
    public  Variable Evaler(string s)
    {
        clac++;
        if (clac > calcmax)
        {
            return null;
        }
        if (s.Contains("(")||s.Contains("<") || s.Contains("=") || s.Contains(">") || s.Contains("!"))
        {
           return Evaler( Calculate(CustomSplitter(s)));
        }
        Variable v = null;
        v = vars.Find(x => x.name == s);
        if (v != null)
        {
            return v;
        }
        return Variable.Evaler(s);
    }
}
[Serializable]
public class Line
{
    public List<string> parts = new List<string>();
    public Line(List<string> p)
    {
        parts = p;
    }

}