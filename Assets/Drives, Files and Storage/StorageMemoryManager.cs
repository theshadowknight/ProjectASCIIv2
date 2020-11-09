using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StorageMemoryManager : MonoBehaviour
{
  
    public List<Drive> drives = new List<Drive>();
    [SerializeReference]
    public File currentFile;
    public static StorageMemoryManager instance;
    public Drive defaultDrive = new Drive("drive0:");
    public TextMeshProUGUI pathText;
    public void ChangePathText()
    {
        pathText.text = TextScreenManager.instance.beginText+ GetCurrentPath() + ">" + TextScreenManager.instance.endText;
    }
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
        if (!PlayerPrefs.HasKey("drives"))
        {
            Directory.CreateDirectory(Application.persistentDataPath
                 + "/Drives");
            drives = new List<Drive>();
            drives.Add(defaultDrive);
            SaveAll();
        }

        LoadAll();

        foreach (Drive d in drives)
        {
            d.SetupFiles();
        }
        

      //  Debug.Log(GetFileFormPath("disk0:/main/tester/poggers/aaa").GetPath());
    }
    public string Pather(string s)
    {
        if (s == "-c")
        {
            return GetCurrentPath();
        }
        else
        {
            return s;
        }
    }
    public void Start()
    {
        ChangePathText();
    }
    public string GetCurrentPath()
    {
        return currentFile.GetPath() + "/" + currentFile.GetFullName();
    }
    public File MakeFile(string namer, string extension, string data="")
    {
        return new File(namer, extension, data);
    }
    public File MakeFileInFile(File parent,string namer, string extension, string data = "")
    {
        File f = MakeFile(namer, extension, data);
        if (parent.files.FindIndex(x => x.name == namer) != -1)
        {
            return null;
        }
        f.parentFile = parent;
        parent.files.Add(f);
        return f;
    }
    public File MakeFileAtPath(string path, string namer, string extension, string data = "")
    {
        File parent = GetFileFormPath(path);
        if (parent == null)
        {
            return null;
        }
        File f = MakeFileInFile(parent, namer, extension, data);
        return f;
    }
    public File GetFileFormPath(string path)
    {
      
        string[] splitted= path.Split('/');
        Drive d = drives.Find(x => x.namer == splitted[0]);
        if (d == null)
        {
            return null;
        }
        if (splitted[splitted.Length - 1] == "..")
        {
            return null;
        }
        File f = d.main;
        List<string> spl = splitted.ToList<string>();
        if (spl.Contains(".."))
        {
            
                f = Finder(f, spl,2);
            
        }
        else {
            for (int a = 2; a < spl.Count; a++)
            {
                f = f.FindFileWithName(spl[a]);
            }
        }
        return f;
    }
    public File Finder(File currentf, List<string> spl,int index)
    {

        //main drive0:/../test 2
     
            for (int i = 0; i < currentf.files.Count; i++)
            {
            if (spl.Count < index)
            {
                return null;
            }
            Debug.LogError(currentf.name + "-" + spl[index] + index);
            File g = null;
            if (spl[index] == "..")
            {
                g = Finder(currentf.files[i], spl, (index + 1));
            }
            else
            {
                g = currentf.FindFileWithName(spl[index]);
            }
            if (g != null)
            {
                Debug.LogError("found");
                return g;
            }
            }
        return null;
    
    }
    public Variable RemoveFileAtPath(string path)
    {
        File f = GetFileFormPath(path);
       if(f.parentFile != null)
        {
           bool b= f.parentFile.files.Remove(f);
            return new Variable("out", VariableType.Bool, b.ToString());

        }
        else if (f.parentDrive != null)
        {
            return new Variable("error",    VariableType.Error,"You can't remove main folder!");
        }
        return new Variable("error", VariableType.Error, "This file doesn't exist on any drives!");

    }
    private void SaveData(Drive save)
    {
        BinaryFormatter bf = new BinaryFormatter();
     
       
        FileStream file = System.IO.File.Create(Application.persistentDataPath
                     + "/Drives/" + save.namer.Replace(":", "") + ".dat");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Saved "+ save.namer);
    }
    private Drive LoadData(string name)
    {
        Drive data = null;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
                   System.IO.File.Open(Application.persistentDataPath
                          + "/Drives/" + name.Replace(":", "") + ".dat",FileMode.Open);
        data = (Drive)bf.Deserialize(file);

        file.Close();

        Debug.Log("Loaded " + data.namer);

        return data;


    }
    public void SaveAll()
    {
        DrivesNames dn = new DrivesNames();
        foreach(Drive d in drives)
        {
            dn.names.Add(d.namer.Replace(":",""));
            SaveData(d);
        }
        PlayerPrefs.SetString("drives", JsonUtility.ToJson(dn));
    }
    public void LoadAll()
    {
        if (PlayerPrefs.HasKey("drives"))
        {
            DrivesNames dn = JsonUtility.FromJson<DrivesNames>(PlayerPrefs.GetString("drives"));
            drives = new List<Drive>();
            foreach (string s in dn.names)
            {
                drives.Add(LoadData(s));
               
            }
            currentFile = drives[0].main;
        }

    }
    public void OnDestroy()
    {
        SaveAll();
    }
    [Serializable]
   public class DrivesNames
    {
        public List<string> names = new List<string>();
    }
}
