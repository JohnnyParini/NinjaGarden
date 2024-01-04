using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Crop")]
public class Crop : ScriptableObject
{
    public int timeToGrow = 4;
    public Item yield;
    public int count = 1;
    public int stage = 1;
    public string type; //What I need to do: I need to 1) determine type. determine stage. use the 
}
