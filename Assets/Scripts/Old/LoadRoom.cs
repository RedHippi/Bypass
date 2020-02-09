using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoom : MonoBehaviour
{
    //Place prefabs for our plain-walls and door-walls here
    public GameObject Wall;
    public GameObject VertDWall;
    public GameObject HorizDWall;

    //Set what walls are loaded here
    public int TopType;
    public int BotType;
    public int RType;
    public int LType;

    //NOTE: Types are: 1-Wall, 2-VertDWall, 3-HorizDWall
    private GameObject FetchObject (int i)
    {
        switch (i)
        {
            case 1: return Wall;

            case 2: return VertDWall;

            case 3: return HorizDWall;

            default: return Wall;
        }
            
    }


    //Helper function for if our wall is a door/wall combination. Grab the wall and then change the scale.
    private void DWallScale (float l, GameObject g,bool b)
    {
        Transform wc = g.GetComponent<DoorWall>().WallT;
        //Debug.Log(wc);
        if(b)       { wc.localScale = new Vector3(l, wc.localScale.y, wc.localScale.z); }
        else /*!b*/ { wc.localScale = new Vector3(wc.localScale.x, l, wc.localScale.z); }
    }

    //Function that will build one of our walls at the given position with relative scaling
    //NOTE: Types are: 1-Wall, 2-VertDWall, 3-HorizDWall
    private void BuildWall (Vector3 pos, float length, int type, bool isHoriz)
    {
        GameObject New = Instantiate(FetchObject(type), pos, new Quaternion(0, 0, 0, 0));
        if (type != 1) { DWallScale(length, New, isHoriz); }
        else //type == 1, or Type is Wall
        {
            Vector3 t = New.transform.localScale;
            if (isHoriz)      { New.transform.localScale = new Vector3(length, t.y, t.z); }
            else /*!isHoriz*/ { New.transform.localScale = new Vector3(t.x, length, t.z); }
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        Vector3 MyPos = transform.position;
        float height = GetComponent<Camera>().orthographicSize;
        float width = Screen.width * height / Screen.height;

        //Build Top Wall
        Vector3 TopPos = new Vector3(MyPos.x, MyPos.y + height, 0f);
        BuildWall(TopPos, width*2, TopType, true);
        //Build Bottom Wall
        Vector3 BotPos = new Vector3(MyPos.x, MyPos.y - height, 0f);
        BuildWall(BotPos, width*2, BotType, true);
        //Build Left Wall
        Vector3 LPos = new Vector3(MyPos.x - width, MyPos.y, 0f);
        BuildWall(LPos, height*2, LType, false);
        //Build Right Wall
        Vector3 RPos = new Vector3(MyPos.x + width, MyPos.y, 0f);
        BuildWall(RPos, height*2, RType, false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
