using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "New Drive", menuName = "Custom/Drive")]

public class Drive : ScriptableObject
{
    public Folder main = new Folder();
}
