using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "boolArr")]
public class boolVals : ScriptableObject
{
    public bool[] vals;
    public int monitored;
    public bool scanOn;
}
