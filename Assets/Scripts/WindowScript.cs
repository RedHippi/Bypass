using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    private float xDiff, yDiff;

    public BoundStorage ScreenBounds;
    private float YTop, YBot, XRight, XLeft;
    [Space(10f)]
    public GameObject WindowMin;
    public GameObject WindowDel;
    public GameObject WindowBody;

    public GameObject IconParent;


    void Start()
    {
        Bounds MyBounds = GetComponent<SpriteRenderer>().sprite.bounds;
        float MyWidth = (MyBounds.max.x - MyBounds.min.x) * transform.localScale.x;
        float MyHeight = (MyBounds.max.y - MyBounds.min.y) * transform.localScale.y;

        (YTop, YBot) = ScreenBounds.Bound1;
        (XRight, XLeft) = ScreenBounds.Bound2;

        YTop -= MyHeight / 2f;
        YBot += MyHeight / 2f;
        XRight -= MyWidth / 2f;
        XLeft += MyWidth / 2f;
    }

    //Range under which this object considers itself to have not moved

    private void OnMouseDown()
    {
        //Get vector that maps mousePosition to thisPosition
        Vector3 ObjPos = GetMousePosition();
        xDiff = ObjPos.x - transform.position.x;
        yDiff = ObjPos.y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        Vector3 ObjPos = GetMousePosition();
        ObjPos = new Vector3(Mathf.Clamp(ObjPos.x - xDiff,XLeft,XRight), Mathf.Clamp(ObjPos.y - yDiff,YBot,YTop), 0f);
        transform.position = ObjPos;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(MousePos);
    }

    public void ReadInput(GameObject g)
    {
        if(g.name == "Mini") { IconParent.GetComponent<IconScript>().ToggleMin(); } //Run minimize on IconParent
        if(g.name == "Close") { IconParent.GetComponent<IconScript>().CloseWindow(); }
    }
}
