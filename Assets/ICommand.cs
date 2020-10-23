using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICommand
{
    string commandWord { get; }
    bool Execute(string[] args);
}