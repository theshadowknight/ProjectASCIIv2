using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class File
{
    public string name;
    [HideInInspector]
    public string extension = "";
    public string data;
    public List<File> files = new List<File>();
    [NonSerialized]

    public File parentFile = null;
    public string GetPath()
    {
        if (parentFile == null)
        {
            return "";
        }
        string s = parentFile.GetPath() + "/" + parentFile.GetFullName();
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
    public virtual void Execute()
    {

    }
    public File(string n, string ext, string d = "")
    {
        name = n;
        extension = ext;
        data = d;
    }
}

