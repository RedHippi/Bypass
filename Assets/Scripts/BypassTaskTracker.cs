using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BypassTaskTracker : MonoBehaviour
{
    [SerializeField] private GameObject TaskDirectory;
    [SerializeField] private boolVals TasksCompleted;

    private void Start()
    {
        for(int i = 0; i < TasksCompleted.vals.Length; i++)
        {
            TasksCompleted.vals[i] = false;
        }
    }
    //When pressed, toggle the view of our internal directory
    public void ToggleDirectory()
    {
        TaskDirectory.SetActive(!TaskDirectory.activeSelf);
    }

    //Our Tasks will tell us when they are selected, changing to the now active one.
    public void SetTask(int taskNum)
    {
        TasksCompleted.monitored = taskNum;
    }

    //
    public void StartCheck()
    {
        TasksCompleted.scanOn = false;
        StopAllCoroutines();
        StartCoroutine(MonitorTask());
    }

    private IEnumerator MonitorTask()
    {
        //Fix our task numbers back to 0-indexing
        TasksCompleted.scanOn = true;
        //On the users next click, we will check to see what the user has clicked.
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        //Give a small buffer of time, and then disable the Scan
        yield return new WaitForSeconds(0.1f);
        TasksCompleted.scanOn = false;
    }



}
