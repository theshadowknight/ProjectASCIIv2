using HelperFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class StorageMemoryManager : MonoBehaviour
{

    public List<Drive> drives = new List<Drive>();
    [HideInInspector]
    [SerializeReference]
    public File currentFile;
    public static StorageMemoryManager instance;
    public Drive defaultDrive = new Drive("drive0:");
    public TextMeshProUGUI pathText;
    [HideInInspector]
   [SerializeReference]
    public File bufferFileMade = null;
     [HideInInspector]
    [SerializeReference]
    public File bufferFileGet = null;
    [HideInInspector]
    private File finderOutput;
    [HideInInspector]
    public Variable removalOutput;

    public string systemPath;
    [SerializeReference]
    public File registry;
    public List<Variable> registryVariables = new List<Variable>();
    [SerializeReference]
    public File system;


    public void ChangePathText()
    {
        pathText.text = CommandLineManager.instance.beginText.Replace("{fontSize}", (ThemeManager.instance.selectedSize.fontSize * (5f / 6f)).ToString("0")) + GetCurrentPath() + ">" + CommandLineManager.instance.endText;
        //    StartCoroutine(TextScreenManager.instance.UpdatePather());    
    }
    public string gett()
    {
        return CommandLineManager.instance.beginText.Replace("{fontSize}", (ThemeManager.instance.selectedSize.fontSize * (5f / 6f)).ToString("0")) + GetCurrentPath() + ">";

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
            d.main.CalculateSize();
        }
        // CalculateSize();

        //  Debug.Log(GetFileFormPath("disk0:/main/tester/poggers/aaa").GetPath());
    }
    public string Pather(string s)
    {
        if (s.Contains("-c"))
        {
            return s.Replace("-c", GetCurrentPath());
        }
        else if (s[0] == '.' && s[1] == '/')
        {
            Debug.Log(s + "  " + GetCurrentPath() + s.Substring(1));
            return GetCurrentPath() + s.Substring(1);
        }
        else if (s[0] == '.' && s[1] == '.')
        {


            //    return GetCurrentPath() + s.Substring(2)
            return GetCurrentPath().Substring(0, GetCurrentPath().LastIndexOf('/')) + s.Substring(2);
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
    public void CalculateSize()
    {
        foreach (Drive d in drives)
        {
            CalculateSizeForFile(d.main);
            d.main.CalculateSize();
        }
    }
    private void CalculateSizeForFile(File f)
    {
        foreach (File fs in f.files)
        {
            fs.CalculateSize();
            CalculateSizeForFile(fs);
        }
    }
    public string GetCurrentPath()
    {
        return currentFile.GetPath() + "/" + currentFile.GetFullName();
    }
    public File MakeFile(string namer, string extension, string data = "")
    {
        return new File(namer, extension, data);
    }
    public IEnumerator FileMaker(string namer, string extension, string data = "")
    {
        yield return new WaitUntil(() => Processor.instance.UseRam(10));
        yield return new WaitUntil(() => Processor.instance.UseRam(namer.Length * 8 + 8 + data.Length * 8));

        bufferFileMade = MakeFile(namer, extension, data);


        yield return new WaitUntil(() => Processor.instance.UseRam(10));

    }
    public IEnumerator FileMakerInFile(File parent, string namer, string extension, string data = "")
    {
        if (parent == null)
        {
            yield return CommandLineManager.instance.Write("[c:12]Making files failed...[c:0]");
            yield break;
        }
        ConnectionTypes t = ConnectionTypes.User;
        int a = Functions.EnumFromRegistryVariables(typeof(ConnectionTypes), "current_user");
        if (a != -1)
        {
            t = (ConnectionTypes)a;
        }
        if (!parent.GetPremission(t, PremissionTypes.Write))
        {
            yield return CommandLineManager.instance.Write("[c:12]Making files failed...[c:0]");
            yield break;
        }
        yield return new WaitUntil(() => Processor.instance.UseRam(10));
        yield return new WaitUntil(() => Processor.instance.UseRam(namer.Length * 8 + 8 + data.Length * 8));
        yield return FileMaker(namer, extension, data);
        bufferFileMade.parentFile = parent;
        bufferFileMade.parentDrive = parent.parentDrive;
        bufferFileMade.SetParentToChildren();
        bufferFileMade.UpdateSize();


        yield return new WaitUntil(() => Processor.instance.UseRam(10));

    }
    public IEnumerator FileMakerAtPath(string path, string namer, string extension, string data = "")
    {
        yield return new WaitUntil(() => Processor.instance.UseRam(10));
        ConnectionTypes t = ConnectionTypes.User;
        int a = Functions.EnumFromRegistryVariables(typeof(ConnectionTypes), "current_user");
        if (a != -1)
        {
            t = (ConnectionTypes)a;
        }

        yield return GetFileFormPath(path, t);
        yield return FileMakerInFile(bufferFileGet, namer, extension, data);

        yield return new WaitUntil(() => Processor.instance.UseRam(10));

    }
   
    public IEnumerator GetFileFormPath(string path, ConnectionTypes ct = ConnectionTypes.System)
    {

        string[] splitted = path.Split('/');
        Drive d = drives.Find(x => x.name == splitted[0]);
        if (d == null)
        {
            bufferFileGet = null;
            yield break;
        }
        if (splitted[splitted.Length - 1] == "..")
        {
            bufferFileGet = null;
            yield break;
        }
        File f = d.main;
        List<string> spl = splitted.ToList<string>();
        if (spl.Contains(".."))
        {
            yield return Finder(f, spl, 2);
            f = finderOutput;
        }
        else
        {
            for (int a = 2; a < spl.Count; a++)
            {
                f = f.FindFileWithName(spl[a]);
                if (!f.GetPremission(ct, PremissionTypes.Read))
                {
                    bufferFileGet = null;
                    yield break;

                }
            }
        }
        bufferFileGet = f;
        yield break;
    }
   
    public IEnumerator Finder(File currentf, List<string> spl, int index, ConnectionTypes ct=ConnectionTypes.System)
    {
        if (!currentf.GetPremission(ct, PremissionTypes.Read))
        {
            finderOutput = null;
            yield break;
        }
        if (spl.Count < index)
        {
            finderOutput = null;
            yield break;
        }
        yield return CommandLineManager.instance.Write("Looking in " + currentf.GetFullName() + "...");

        if (spl[index] != "..")
        {
            finderOutput = currentf.FindFileWithName(spl[index]);
            yield break;
        }
        for (int i = 0; i < currentf.files.Count; i++)
        {
            File g = null;
            if (spl[index] == "..")
            {
                yield return Finder(currentf.files[i], spl, (index + 1));
                g = finderOutput;
            }
            else
            {
                g = currentf.FindFileWithName(spl[index]);
            }
            if (g != null)
            {
                finderOutput = g;
                yield break;
            }
        }
        finderOutput = null;
        yield break;

    }
    public IEnumerator RemoveFileAtPath(string path)
    {

        yield return GetFileFormPath(path);
        File f = bufferFileGet;
        if (f.parentFile != null)
        {
            bool b = f.parentFile.files.Remove(f);
            removalOutput = new Variable("out", VariableType.Bool, b.ToString());
            yield break;
        }
        else if (f.parentDrive != null)
        {
            removalOutput = new Variable("error", VariableType.NULL, "You can't remove main folder!");
            yield break;
        }
        removalOutput = new Variable("error", VariableType.NULL, "This file doesn't exist on any drives!");
        yield break;
    }
    private void SaveData(Drive save)
    {
        BinaryFormatter bf = new BinaryFormatter();


        FileStream file = System.IO.File.Create(Application.persistentDataPath
                     + "/Drives/" + save.name.Replace(":", "") + ".dat");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Saved " + save.name);
    }
    private Drive LoadData(string name)
    {
        Drive data = null;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
                   System.IO.File.Open(Application.persistentDataPath
                          + "/Drives/" + name.Replace(":", "") + ".dat", FileMode.Open);
        data = (Drive)bf.Deserialize(file);

        file.Close();

        Debug.Log("Loaded " + data.name);

        return data;


    }
    public void SaveAll()
    {

        DrivesNames dn = new DrivesNames();
        foreach (Drive d in drives)
        {
            dn.names.Add(d.name.Replace(":", ""));
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
