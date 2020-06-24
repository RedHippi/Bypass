using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchChild : MonoBehaviour
{
    private bool On;

    //Must have itself marked as an affected index.
    public int[] AffectedIndices;

    private int MyIndex;

    public GameObject MyManager;

    public Sprite Enabled, Disabled;

    // Start is called before the first frame update
    void Start()
    {
        On = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (On)
            GetComponent<SpriteRenderer>().sprite = Enabled;
        else
            GetComponent<SpriteRenderer>().sprite = Disabled;
    }

    public void ToggleOn()
    {
        On = !On;

    }

    private bool HasSelf()
    {
        bool check = false;
        foreach(int i in AffectedIndices)
        {
            if (i == MyIndex)
                check = true;
        }
        return check;

    }

    public void SetIndex(int i)
    {
        MyIndex = i;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!On && collision.gameObject.CompareTag("Player"))
        {
            MyManager.GetComponent<SwitchManager>().UpdateSwitches(AffectedIndices);
        }
    }
}
