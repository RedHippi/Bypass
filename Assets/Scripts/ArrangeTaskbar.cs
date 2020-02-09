using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeTaskbar : MonoBehaviour
{

    public GameObject IconRepRef;
    public GameObject WPoint;

    private GameObject[] RepPositions;

    private int RepNum, NumUsed;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize length and location of RepPositions relative to Taskbar.width/IconRepRef.width

        //Get Number of Representatives we can fit in the taskbar [Referred to as RefNum]
        Bounds MyBounds = GetComponent<SpriteRenderer>().sprite.bounds;
        float MyWidth = (MyBounds.max.x - MyBounds.min.x) * transform.localScale.x;
        Bounds IBounds = IconRepRef.GetComponent<SpriteRenderer>().sprite.bounds;
        float IWidth = (IBounds.max.x - IBounds.min.x) * IconRepRef.transform.localScale.x;
        RepNum = (int)(MyWidth / IWidth);

        //Set up our WPoint's. 1st point is placed based off of the size of our IconRepRef, subsequent use the IWidth.
        RepPositions = new GameObject[RepNum];
        for(int i=0; i < RepNum ; i++)
        {
            if (i == 0)
            {
                Vector3 NewPos = new Vector3(transform.position.x - (MyWidth / 2f) + 0.5f + (IWidth / 2f), transform.position.y, transform.position.z);
                RepPositions[0] = Instantiate(WPoint, NewPos, transform.rotation);
                RepPositions[0].SetActive(false);
            } 
            else
            {
                Vector3 NewPos = new Vector3(RepPositions[i - 1].transform.position.x + IWidth, transform.position.y, transform.position.z);
                RepPositions[i] = Instantiate(WPoint, NewPos, transform.rotation);
                RepPositions[i].SetActive(false);
            }
        }
        

    }

    //HELPER FUNCTIONS FOR REFERENCING RepPositions
    private GameObject HeldGet(int index) { return RepPositions[index].GetComponent<WayPointInfo>().Held; }
    private GameObject OverGet(int index) { return RepPositions[index].GetComponent<WayPointInfo>().Over; }
    private void HeldSet(int index, GameObject g) { RepPositions[index].GetComponent<WayPointInfo>().Held = g; }
    private void OverSet(int index, GameObject g) { RepPositions[index].GetComponent<WayPointInfo>().Over = g; }
    private void LeftSet(int index, GameObject g) { RepPositions[index].GetComponent<WayPointInfo>().Left = g; }

    //Temporarily constructed functions to facilitate communication between icons and taskbar.
    public void AddIconRep(GameObject NewRep)
    {
        if(NumUsed < RepNum)
        {
            RepPositions[NumUsed].SetActive(true);
            NewRep.transform.position = RepPositions[NumUsed].transform.position;
            NumUsed++;
        }

    }

    public void RemoveIconRep()
    {
        NumUsed--;
        for(int i = 0; i<NumUsed; i++)
        {
            if(HeldGet(i) == null)
            {
                GameObject tmp = HeldGet(i + 1);
                HeldSet(i + 1, null);
                HeldSet(i, tmp);
            }
        }
        RepPositions[NumUsed].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Check all WPoint's. Check for the sake of moving icons between them.
        for(int i=0;i < NumUsed;i++)
        {
            if (HeldGet(i) == null)
            {
                if(i > 0 && OverGet(i-1) != null)
                {
                    LeftSet(i, null);
                    GameObject tmp = HeldGet(i - 1);
                    HeldSet(i - 1, OverGet(i - 1));
                    OverSet(i - 1, null);
                    HeldSet(i, tmp);
                }
                else if (i<RepNum-1 && OverGet(i+1) != null)
                {
                    LeftSet(i, null);
                    GameObject tmp = HeldGet(i + 1);
                    HeldSet(i + 1, OverGet(i + 1));
                    OverSet(i + 1, null);
                    HeldSet(i, tmp);
                }
            }
        }
    }
}
