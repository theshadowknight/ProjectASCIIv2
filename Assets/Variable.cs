using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum VariableType { String,Int,Float,Double,Byte,Short,Bool,Char,Long,Error}
public enum CompareType {  }

public class Variable : MonoBehaviour
{
    public string name;
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
                                    
                    return int.Parse(data)>int.Parse(v.data);
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
                    return bool.Parse(data)==bool.Parse(v.data);
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
}

