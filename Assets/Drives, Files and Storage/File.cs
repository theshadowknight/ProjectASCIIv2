using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class File
{
    public string name;
  //  [HideInInspector]
    public string extension = "";
    public string data;
    [SerializeField]
    public List<File> files = new List<File>();
    [NonSerialized]
    public File parentFile = null;
    [NonSerialized]
    public Drive parentDrive = null;
    public long size =-1;
    public string GetPath()
    {
        string s = "";
        if (parentFile == null)
        {
            return parentDrive.namer ;
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
            size = 8L * ((long)data.Length);
        }
        return size;
    }
}

