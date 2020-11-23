using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum VariableType { String,Int,Float,Double,Byte,Short,Bool,Char,Long,NULL}
public enum CompareType { IsGreaterThan, IsEqualTo , IsLessThan , IsLessOrEqualThan, IsGreaterOrEqualThan,IsDiffrent }

[System.Serializable]
public class Variable
{
    public  string name;
    public string data;
    public VariableType type;
  
    public bool IsGreaterThan(Variable v)
    {
        switch (type)
        {
            case VariableType.Bool:
                {
                    int a = bool.Parse(data).CompareTo(bool.Parse(v.data));

                    if (a > 0)
                    {
                        return true;
                    }
                    else if (a < 0)
                    {
                        return false;
                    }
                  
                        return false;

                   
                }
            case VariableType.Int:
                {
                                    
                    return int.Parse(data) >int.Parse(v.data);
                }
            case VariableType.Double:
                {
                   return double.Parse(data) > double.Parse(v.data);
                }
            case VariableType.Float:
                {
                    return float.Parse(data) > float.Parse(v.data);
                }
            case VariableType.Byte:
                {
                    return byte.Parse(data) > byte.Parse(v.data);
                }
            case VariableType.Char:
                {
                    return char.Parse(data) > char.Parse(v.data);
                }
            case VariableType.Short:
                {
                    return short.Parse(data) > short.Parse(v.data);
                }
            case VariableType.String:
                {
                    return data.Length > v.data.Length;
                }
            case VariableType.Long:
                {
                    return long.Parse(data) > long.Parse(v.data);
                }
        }
        return false;
    }
    public bool IsEqualTo(Variable v)
    {
        switch (type)
        {
            case VariableType.Bool:
                {
                    
                    return bool.Parse(data) == bool.Parse(v.data) ;
                }
            case VariableType.Int:
                {
                    return int.Parse(data) == int.Parse(v.data);
                }
            case VariableType.Double:
                {
                    return double.Parse(data) == double.Parse(v.data);
                }
            case VariableType.Float:
                {
                    return float.Parse(data) == float.Parse(v.data);
                }
            case VariableType.Byte:
                {
               
                    return byte.Parse(data) == byte.Parse(v.data);
                }
            case VariableType.Char:
                {
                    return char.Parse(data) == char.Parse(v.data);
                }
            case VariableType.Short:
                {
                    return short.Parse(data) == short.Parse(v.data);
                }
            case VariableType.String:
                {
                    return data.Equals(v.data);
                }
            case VariableType.Long:
                {
                    return long.Parse(data) == long.Parse(v.data);
                }
        }
        return false;
    }
    public bool IsLessThan(Variable v)
    {
        switch (type)
        {
            case VariableType.Bool:
                {
                    int a = bool.Parse(data).CompareTo(bool.Parse(v.data));
                    if (a > 0)
                    {
                        return false;
                    }
                    else if (a < 0)
                    {
                        return true;
                    }
                    return false;
                }
            case VariableType.Int:
                {
                    
                    return int.Parse(data) < int.Parse(v.data);
                }
            case VariableType.Double:
                {
                    return double.Parse(data) < double.Parse(v.data);
                }
            case VariableType.Float:
                {
                    return float.Parse(data) < float.Parse(v.data);
                }
            case VariableType.Byte:
                {
                    return byte.Parse(data) < byte.Parse(v.data);
                }
            case VariableType.Char:
                {
                    return char.Parse(data) < char.Parse(v.data);
                }
            case VariableType.Short:
                {
                    return short.Parse(data) < short.Parse(v.data);
                }
            case VariableType.String:
                {
                    return data.Length < v.data.Length;
                }
            case VariableType.Long:
                {
                    return long.Parse(data) < long.Parse(v.data);
                }
        }
        return false;
    }
    public bool IsLessOrEqualThan(Variable v) {
        return IsLessThan(v) || IsEqualTo(v);
    }
    public bool IsGreaterOrEqualThan(Variable v)
    {
        return IsGreaterThan(v) || IsEqualTo(v);
    }
    public Variable(string n, VariableType t,string d)
    {
        name = n;
        type = t;
        data = d;
    }
    public bool CheckType()
    {  
        switch (type)
        {
            case VariableType.Bool: 
                {
                    return bool.TryParse(data, out _);
                }
            case VariableType.Int:
                {
                    return int.TryParse(data, out _);
                }
            case VariableType.Double:
                {
                    return double.TryParse(data, out _);
                }
            case VariableType.Float:
                {
                    return float.TryParse(data, out _);
                }
            case VariableType.Byte:
                {
                    return byte.TryParse(data, out _);
                }
            case VariableType.Char:
                {    
                    return char.TryParse(data, out _);
                }
            case VariableType.Short:
                {
                    return short.TryParse(data, out _);
                }
            case VariableType.String:
                {
                    return true;
                }
            case VariableType.Long:
                {
                    return long.TryParse(data, out _);
                }
        }
        return false;
    }
    public bool AutoCompare(CompareType ct, Variable v2)
    {
        switch (ct)
        {
            case CompareType.IsEqualTo:
                {
                    return IsEqualTo(v2);
                }
            case CompareType.IsDiffrent:
                {
                    return !IsEqualTo(v2);
                }
            case CompareType.IsGreaterOrEqualThan:
                {
                    return IsGreaterOrEqualThan(v2);
                }
            case CompareType.IsGreaterThan:
                {
                    return IsGreaterThan(v2);
                }
            case CompareType.IsLessOrEqualThan:
                {
                    return IsLessOrEqualThan(v2);
                }
            case CompareType.IsLessThan:
                {
                    return IsLessThan(v2);
                }
        }
        return false;
    }
    public static Variable Evaler(string s)
    {
        if (int.TryParse(s, out _))
        {
            return new Variable("", VariableType.Int, s);
        }
        string cut = s.Substring(0, s.Length - 1);

        if (s[s.Length - 1] == 'd')
        {
            if (double.TryParse(cut, out _))
            {
                return new Variable("", VariableType.Double, cut);
            }
        }
        if (s[s.Length - 1] == 'f')
        {
            if (float.TryParse(cut, out _))
            {
                return new Variable("", VariableType.Float, cut);
            }
        }
        string cutter = s.Substring(1, s.Length - 2);

        if (s[s.Length - 1] == '\'' && s[0] == '\'')
        {
            
                return new Variable("", VariableType.Char, cutter);
            
        }
        if (s[s.Length - 1] == '"' && s[0] == '"')
        {
            
                return new Variable("", VariableType.String, cutter);
            
        }
        if (s[s.Length - 1] == 's')
        {
            if (short.TryParse(cut, out _))
            {
                return new Variable("", VariableType.Short, cut);
            }
        }
        if (s[s.Length - 1] == 'b')
        {
            if (byte.TryParse(cut, out _))
            {
                return new Variable("", VariableType.Byte, cut);
            }
        }
        if (s == "true" || s == "false")
        {
           
                return new Variable("", VariableType.Bool, s);
            
        }
        if (s[s.Length - 1] == 'L')
        {
            if (short.TryParse(cut, out _))
            {
                return new Variable("", VariableType.Long, cut);
            }
        }
       
        return null;
    }
  //  public void GetNumericValue
   

}

