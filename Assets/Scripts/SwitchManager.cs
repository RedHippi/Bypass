using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{

    private bool Solved = false;
    private bool[] SwitchVals;
    static private int num_switches = 5;


    public GameObject[] Switches = new GameObject[num_switches];

    private void Awake()
    {
        for(int i = 0; i < Switches.Length; i++)
        {
            Switches[i].GetComponent<SwitchChild>().SetIndex(i);
            Switches[i].GetComponent<SwitchChild>().MyManager = gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchVals = new bool[num_switches];
        for(int i = 0; i < num_switches; i++)
        {
            SwitchVals[i] = false;
        }
        StartCoroutine(WaitForSolved());
        Debug.Log(Solved);
    }

    public void UpdateSwitches(int[] indices)
    {
        if (!Solved)
        {
            foreach (int i in indices)
            {
                SwitchVals[i] = !SwitchVals[i];
                Switches[i].GetComponent<SwitchChild>().ToggleOn();
            }
            if (AllTrue())
                Solved = true;
        }
    }

    private bool AllTrue()
    {
        foreach(bool s in SwitchVals)
        {
            if (!s)
                return false;
        }
        return true;
    }


    private IEnumerator WaitForSolved()
    {
        yield return new WaitUntil(() => Solved);
        //Do something cool here
        Debug.Log("Hey, you solved it!");
    }
}
