using System.Collections.Generic;
using UnityEngine;

public class StorageMemoryManager : MonoBehaviour
{
    public Drive drive0;
    public List<Drive> drives = new List<Drive>();
    public File currentFile;
    public static StorageMemoryManager instance;

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
        currentFile = drive0.main;
        drive0.main.SetParentToChildren();
        foreach (Drive d in drives)
        {
            d.main.SetParentToChildren();
        }
    }
    public string GetCurrentPath()
    {
        return currentFile.GetPath() + "/" + currentFile.GetFullName();
    }
    public File MakeFile(string name, string extension, string data="")
    {
        return new File(name, extension, data);
    }
    public File MakeFileHere()
}
