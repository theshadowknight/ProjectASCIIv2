using System;
using System.Collections.Generic;
using UnityEngine;
using HelperFunctions;
[Serializable]
public class File
{
    public string name;
  //  [HideInInspector]
    public string extension = "";
   [Multiline(3)]
    public string data;
    public string premissionValue = "111111111";

    [SerializeField]
    public List<File> files = new List<File>();
    [NonSerialized]
    public File parentFile = null;
    [NonSerialized]
    public Drive parentDrive = null;
    public long size =-1;
    public bool staticSize=false;
    public string GetPath()
    {
        string s = "";
        if (parentFile == null)
        {
            if (parentDrive == null)
            {
                return "how?";
            }
            return parentDrive.name ;
        }
     s += parentFile.GetPath() + "/" + parentFile.GetFullName();
        return s;
    }
    public string GetFullName()
    {
        return name + (extension == "" ? "" : ("." + extension));
    }
    public void SetParentToChildren()
    {
        foreach (File f in files)
        {
            f.parentFile = this;
            f.SetParentToChildren();
        }
    }
   /* public virtual void Execute()
    {

    }*/
    public File(string n, string ext, string d = "")
    {
        name = n;
        extension = ext;
        data = d;
    }
    public File FindFileWithName(string namer)
    {
        
        return files.Find(x => x.GetFullName() == namer);
    }
    public long GetSize()
    {
        if (size == -1)
        {
            CalculateSize();
        }
        return size;
    }
    public long GetSize(bool b)
    {
        if (size == -1||b)
        {
            CalculateSize();
        }
        return size;
    }
    public void CalculateSize()
    {
        if (staticSize) { return; }
            size = 8L * ((long)data.Length)+ (8L * ((long)data.Length))%128;
        if (size == 0)
        {
            size = 128L;
        }
       foreach(File f in files)
        {
            size += f.GetSize(true);
        }
       
    }
    public void UpdateSize()
    {
        CalculateSize();
        if (parentFile != null)
        {
            parentFile.UpdateSize();
        }
    }
    public void SetPremission(ConnectionTypes ct,PremissionTypes pt, bool v)
    {
        int a = 0;
        a += (int)ct* 3+(int)pt;
        char[] cs = premissionValue.ToCharArray();
        cs[a] = v ? '1' : '0';
        premissionValue = new string(cs);     
    }
    public bool GetPremission(ConnectionTypes ct, PremissionTypes pt)
    {
        int a = 0;
        a += (int)ct * 3 + (int)pt;
        char[] cs = premissionValue.ToCharArray();

        return cs[a] == '1';
    }
    public string GetPremissionStr()
    {

        string p = "";
        for (int l = 0; l < premissionValue.Length; l++)
        {
            if (premissionValue[l] == '0')
            {
                p += "-";
                continue;
            }
            if (l % 3 == 0)
            {
                p += "r";
            }
            else if (l % 3 == 1)
            {
                p += "w";
            }
            else
            {
                p += "x";
            }


        }
        return p;
    }
}

