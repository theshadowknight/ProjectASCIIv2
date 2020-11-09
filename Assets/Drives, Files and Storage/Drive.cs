using System;
using UnityEngine;
[Serializable]
//[CreateAssetMenu(fileName = "New Drive", menuName = "Custom/Drive")]

public class Drive//: ScriptableObject
{
    public string namer;
    [SerializeField]
    public File main = new File("main","");
    public void SetupFiles()
    {
        looper(main);
        main.SetParentToChildren();
    }
    private void looper(File f)
    {
        f.parentDrive = this;
        foreach (File fs in f.files)
        {

            looper(fs);
        }
    }
    public Drive(string n)
    {
        namer = n;
    }
}
