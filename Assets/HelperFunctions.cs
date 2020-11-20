using System;
using System.Collections.Generic;
using UnityEngine;
namespace HelperFunctions
{
    public enum ConnectionTypes { System, User, Outside }
    public enum PremissionTypes { Read, Write, Execute }

    public class Functions
    {
        public static List<string> segement = new List<string> {"1111110", "0110000","1101101","1111001", "0110011" ,"1011011","1011111","1110000","1111111","1111011"};
        public static string SizeConventer(long l)
        {
            if (l > 1073741824)
            {
                return (l / 1073741824).ToString("0") + "GB";
            }
            else if (l > 1048576)
            {
                return (l / 1048576).ToString("0") + "MB";
            }
            else if (l > 1024)
            {
                return (l / 1024).ToString("0") + "KB";
            }
            return l.ToString("0") + "B";


        }
        public static string IntToHex(int i)
        {
            return i.ToString("X");
        }
        public static List<Variable> JsonVariablesFromFile(File f)
        {
            VariableBlock vb = JsonUtility.FromJson<VariableBlock>(f.data);
            if (vb == null)
            {
                return null;
            }
            return vb.variables;
        }
        public static void JsonVariablesToFile(File f, List<Variable> vs)
        {
            VariableBlock vb = new VariableBlock(vs);
            f.data = JsonUtility.ToJson(vb);
            f.UpdateSize();
        }
        public static List<Variable> TagVariablesFromFile(File f)
        {
            string[] s = f.data.Split('\n');
            List<Variable> vs = new List<Variable>();
            foreach (string str in s)
            {
                if (str[0] == '#')
                {
                    continue;
                }
                string namer = str.Substring(0, str.IndexOf(": "));
                string typer = "";
                string datar = str.Substring(str.IndexOf(": ") + 2);

                if (str[0] == '(')
                {

                    namer = str.Substring(str.IndexOf(") ") + 2, str.IndexOf(": ") - 2 - str.IndexOf(") "));
                    typer = str.Substring(str.IndexOf("(") + 1, str.IndexOf(") ") - 1 - str.IndexOf("("));

                }
                //    Debug.LogError(str+"-namer:" + namer + "\ntyper:" + typer + "\ndatar:" + datar);
                vs.Add(new Variable(namer, GetVariableTypeFromString(typer), datar));
            }

            return vs;
        }
        public static void TagVariablesToFile(File f, List<Variable> vs)
        {
            f.data = TagVariablesToString(vs);
            f.UpdateSize();
        }
        public static string TagVariablesToString(List<Variable> vs)
        {
            string data = "";
            foreach (Variable v in vs)
            {
                data += string.Format("({0}) {1}: {2}", VariableTypeToString(v.type, true), v.name, v.data);


            }
            return data;
        }
        public static int HexToInt(string s)
        {

            return Convert.ToInt32(s, 16);

        }
        public static VariableType GetVariableTypeFromString(string s)
        {
            switch (s)
            {
                case "str":
                case "string":
                    {
                        return VariableType.String;
                    }
                case "s":
                case "short":
                    {
                        return VariableType.Short;
                    }
                case "i":
                case "int":
                    {
                        return VariableType.Int;
                    }
                case "d":
                case "double":
                    {
                        return VariableType.Double;
                    }
                case "f":
                case "float":
                    {
                        return VariableType.Float;
                    }
                case "b":
                case "bool":
                    {
                        return VariableType.Bool;
                    }
                case "B":
                case "byte":
                    {
                        return VariableType.Byte;
                    }
                case "c":
                case "char":
                    {
                        return VariableType.Char;
                    }
                case "l":
                case "long":
                    {
                        return VariableType.Long;
                    }
                default:
                    {
                        return VariableType.NULL;
                    }
            }
            return VariableType.NULL;
        }
        public static string VariableTypeToString(VariableType vt, bool shortest)
        {
            switch (vt)
            {
                case VariableType.Bool:
                    {
                        if (shortest)
                        {
                            return "b";
                        }
                        else
                        {
                            return "bool";

                        }
                    }
                case VariableType.Byte:
                    {
                        if (shortest)
                        {
                            return "B";
                        }
                        else
                        {
                            return "byte";

                        }
                    }
                case VariableType.Char:
                    {
                        if (shortest)
                        {
                            return "c";
                        }
                        else
                        {
                            return "char";

                        }
                    }
                case VariableType.Double:
                    {
                        if (shortest)
                        {
                            return "d";
                        }
                        else
                        {
                            return "double";

                        }
                    }
                case VariableType.Float:
                    {
                        if (shortest)
                        {
                            return "f";
                        }
                        else
                        {
                            return "float";

                        }
                    }
                case VariableType.Int:
                    {
                        if (shortest)
                        {
                            return "i";
                        }
                        else
                        {
                            return "int";

                        }
                    }
                case VariableType.Long:
                    {
                        if (shortest)
                        {
                            return "l";
                        }
                        else
                        {
                            return "long";

                        }
                    }
                case VariableType.Short:
                    {
                        if (shortest)
                        {
                            return "s";
                        }
                        else
                        {
                            return "short";

                        }
                    }
                case VariableType.String:
                    {
                        if (shortest)
                        {
                            return "str";
                        }
                        else
                        {
                            return "string";

                        }
                    }
                default:
                    {
                        return "error";
                    }

            }
            return "";
        }
        public static int EnumFromRegistryVariables(Type t, string namer)
        {
            Variable v = StorageMemoryManager.instance.registryVariables.Find(x => x.name == namer);
            if (v != null)
            {
                int a = 0;
                if (int.TryParse(v.data, out a))
                {
                    if (a < Enum.GetNames(t).Length)
                    {

                        return a;

                    }
                }
            }

            return -1;

        }
    }
    [Serializable]
    public class VariableBlock
    {
        public List<Variable> variables = new List<Variable>();
        public VariableBlock(List<Variable> v)
        {
            variables = v;

        }
    }
}