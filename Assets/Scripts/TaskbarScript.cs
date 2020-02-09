using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskbarScript : MonoBehaviour
{
    //Data passed around to communicate difference in mousePosition and thisPosition.
    private float xDiff;

    //Information pertaining to the bounds of our screen (used to bound movement).
    public BoundStorage ScreenBounds;
    private float XRight,XLeft;

    public bool IsHeld { get; set; }
    public bool Pressed { get; set; }

    public GameObject IconParent;

    private Vector2 mouseStart;

    void Start()
    {
        (XRight, XLeft) = ScreenBounds.Bound2;
        float width = transform.localScale.x * (GetComponent<SpriteRenderer>().sprite.bounds.max.x - GetComponent<SpriteRenderer>().sprite.bounds.min.x);
        XRight -= (width / 2f)+0.5f;
        XLeft += (width / 2f)+0.5f;
        //Debug.Log("Xl and Xr");
        //Debug.Log((XLeft, XRight));

    }
    private void OnMouseDown()
    {
        //Get vector that maps mousePosition to thisPosition
        Vector3 ObjPos = GetMousePosition();
        mouseStart = ObjPos;
        xDiff = ObjPos.x - transform.position.x;

        IsHeld = true;
    }

    private void OnMouseDrag()
    {
        Vector3 ObjPos = GetMousePosition();
        ObjPos = new Vector3(Mathf.Clamp(ObjPos.x - xDiff,XLeft,XRight), transform.position.y, 0f);
        transform.position = ObjPos;
    }

    private void OnMouseUp()
    {
        if (NotMoved(mouseStart, GetMousePosition()))
            IconParent.GetComponent<IconScript>().ToggleMin();
        IsHeld = false;
    }

    //Range under which this object considers itself to have not moved
    private float range = 0.05f;

    private bool NotMoved(Vector2 start, Vector2 end)
    {
        return (start.x - range <= end.x && end.x <= start.x + range)
            && (start.y - range <= end.y && end.y <= start.y + range);
    }


    private Vector3 GetMousePosition()
    {
        Vector3 MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(MousePos);
    }
}
