using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    [SerializeField] private boolVals Tasks;
    [Space(20)]
    public int TaskNum;

    public void TaskAttempt()
    {
        if(Tasks.scanOn)
        {
            if (Tasks.monitored == TaskNum)
            {
                Tasks.vals[TaskNum] = true;
            }
            else
            {
                Tasks.vals[TaskNum] = false;
            }
        }
    }
}
