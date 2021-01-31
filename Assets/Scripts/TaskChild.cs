using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskChild : MonoBehaviour
{
    [SerializeField] private GameObject MyTracker;
    [SerializeField] private boolVals boolArr;
    [Space(20)]
    public int MyTaskNum;

    private Color NotDone;
    private Color Done;

    private void Start()
    {
        NotDone = new Vector4(0.9f, 0.4f, 0.4f, 1f);
        Done = new Vector4(0.4f, 0.9f, 0.4f, 1f);
    }
    private void Update()
    {
        if(boolArr.vals[MyTaskNum])
        {
            GetComponent<Image>().color = Done;
        }
        else
        {
            GetComponent<Image>().color = NotDone;
        }
    }

    public void UpdateTracker()
    {
        MyTracker.GetComponent<BypassTaskTracker>().SetTask(MyTaskNum);
    }
}
