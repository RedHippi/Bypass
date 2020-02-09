using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour
{

    private float xDiff;
    private float yDiff;
    private bool isSelected;

    public GameObject MyWindow;
    public GameObject MyIconRep;

    //Reference to local Taskbar
    private GameObject OurTaskbar;

    private bool isOver, canOpen, Open, Toggle;

    //Information about the borders of our desktop.
    public BoundStorage ScreenBounds;
    private float YTop, YBot, XRight, XLeft;


    public GameObject Select;

    private Vector2 mouseStart;

    //Determines the separation of our icons.
    private float IconIndex = 4.5f;

    // Start is called before the first frame update
    private void Start()
    {
        OurTaskbar = GameObject.Find("Taskbar");

        transform.position = new Vector2(Mathf.Floor(transform.position.x / IconIndex) * IconIndex
                                       , Mathf.Floor(transform.position.y / IconIndex) * IconIndex);

        (YTop, YBot) = ScreenBounds.Bound1;
        (XRight, XLeft) = ScreenBounds.Bound2;

        Bounds MyBounds = GetComponent<SpriteRenderer>().sprite.bounds;
        float MyWidth = (MyBounds.max.x - MyBounds.min.x) * transform.localScale.x;
        float MyHeight = (MyBounds.max.y - MyBounds.min.y) * transform.localScale.y;


        YTop = Mathf.Round(YTop - (MyHeight / 2f)) / IconIndex * IconIndex;
        YBot = Mathf.Round(YBot + (MyHeight / 2f)) / IconIndex * IconIndex;
        XRight = Mathf.Round(XRight - (MyWidth / 2f)) / IconIndex * IconIndex;
        XLeft = Mathf.Round(XLeft + (MyWidth / 2f)) / IconIndex * IconIndex;
    }

    private IEnumerator MonitorSelected()
    {
        while (isSelected)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver) { isSelected = false; }
            yield return null;
        }
    }

    private void Update()
    {
        Select.SetActive(isSelected);
    }

    private void OnMouseDown()
    {
        StopCoroutine(MonitorSelected());
        if (!isSelected)
        {
            isSelected = true;
        }
        else
        {
            canOpen = true;    
        }
        //Get vector that maps mousePosition to thisPosition
        Vector3 ObjPos = GetMousePosition();
        mouseStart = ObjPos;
        xDiff = ObjPos.x - transform.position.x;
        yDiff = ObjPos.y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        Vector3 ObjPos = GetMousePosition();
        ObjPos = new Vector3(Mathf.Clamp(ObjPos.x-xDiff,XLeft,XRight), Mathf.Clamp(ObjPos.y-yDiff,YBot,YTop), 0f);
        transform.position = ObjPos;
    }

    private void OnMouseEnter()
    {
        isOver = true;
    }

    private void OnMouseExit()
    {
        isOver = false;
    }

    

    private void OnMouseUp()
    {
        Vector2 ObjPos = GetMousePosition();
        if (canOpen && NotMoved(mouseStart,ObjPos) && !Open) { OpenWindow();}
        transform.position = new Vector2( Mathf.Round(transform.position.x / IconIndex) * IconIndex
                           ,  Mathf.Round(transform.position.y / IconIndex) * IconIndex);
        canOpen = false;
        StartCoroutine(MonitorSelected());
    }

    private Vector3 GetMousePosition()
    {
        Vector3 MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(MousePos);
    }

    //Range under which this object considers itself to have not moved
    private float range = 0.05f;

    private bool NotMoved(Vector2 start,Vector2 end)
    {
        return (start.x - range <= end.x && end.x <= start.x + range)
            && (start.y - range <= end.y && end.y <= start.y + range);
    }

    //References we watch during MonitorWindow to alter the state of the window/icon based off of user input.
    private GameObject WatchThis;
    private GameObject AndThis;

    private void OpenWindow()
    {
        Open = true;
        /*TODO Open Window!!*/

        WatchThis = Instantiate(MyWindow, Vector3.zero, Quaternion.identity);
        AndThis = Instantiate(MyIconRep, Vector3.zero, Quaternion.identity);
        WatchThis.GetComponent<WindowScript>().IconParent = gameObject;
        AndThis.GetComponent<TaskbarScript>().IconParent = gameObject;
        OurTaskbar.GetComponent<ArrangeTaskbar>().AddIconRep(AndThis);
    }

    public void CloseWindow()
    {
        if (!Open) return;
        StopAllCoroutines();
        Destroy(WatchThis);
        Destroy(AndThis);
        OurTaskbar.GetComponent<ArrangeTaskbar>().RemoveIconRep();
        Open = false;
    }

    private bool IsMin;
    private Vector3 LastPos;
    private Vector3 LastScale;

    public void ToggleMin()
    {
        IsMin = !IsMin;
        if (IsMin) //Start not Min, need to pull it in
        {
            LastPos = WatchThis.transform.position;
            LastScale = WatchThis.transform.localScale;
            StartCoroutine(MinIn());
        }
        else // Start Min, need to get out of my IconRep
        {
            StartCoroutine(MinOut());
        }
    }

    const float MoveTime = 0.1f;

    private IEnumerator MinIn()
    {
        float time = MoveTime;
        Debug.Log(MoveTime);
        float TravelDistance = Vector3.Distance(WatchThis.transform.position, AndThis.transform.position);
        while (IsMin && time > 0.0f)
        {
            WatchThis.transform.position = Vector3.MoveTowards(WatchThis.transform.position,
                                                               AndThis.transform.position,
                                                               TravelDistance*(Time.deltaTime/MoveTime));
            WatchThis.transform.localScale = LastScale * (time/MoveTime);
            time -= Time.deltaTime;
            Debug.Log(time);
            yield return null;
        }
        WatchThis.transform.position = AndThis.transform.position;
        WatchThis.transform.localScale = Vector3.zero;
        yield return null;
    }

    private IEnumerator MinOut()
    {
        float time = MoveTime;
        float TravelDistance = Vector3.Distance(WatchThis.transform.position, LastPos);
        while (!IsMin && time > 0.0f)
        {
            WatchThis.transform.position = Vector3.MoveTowards(WatchThis.transform.position,
                                                               LastPos, TravelDistance * (Time.deltaTime/MoveTime));
            WatchThis.transform.localScale = LastScale * (1-(time/MoveTime));
            time -= Time.deltaTime;
            yield return null;
        }
        WatchThis.transform.position = LastPos;
        WatchThis.transform.localScale = LastScale;

        yield return null;
    }

}
