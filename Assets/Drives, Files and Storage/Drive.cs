using System;
using UnityEngine;
[Serializable]
//[CreateAssetMenu(fileName = "New Drive", menuName = "Custom/Drive")]

public class Drive//: ScriptableObject
{
    public string name;
    [SerializeField]
    public File main = new File("main","");
    public void SetupFiles()
    {
        looper(main);
        main.SetParentToChildren();
        Debug.Log(main.parentDrive.name);

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
        name = n;
    }
}
