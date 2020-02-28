using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "bound")]
public class BoundStorage : ScriptableObject
{
    public (float, float) Bound1;
    public (float, float) Bound2;
}
