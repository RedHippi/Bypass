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
        Debug.Log("Called it");
        if(Tasks.scanOn)
        {
            Debug.Log("Made it past check");
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
