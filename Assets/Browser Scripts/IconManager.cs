using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconManager : MonoBehaviour
                         , IPointerClickHandler
                         , IDragHandler
                         , IEndDragHandler
{

    private bool selected = false;
    private GameObject icon;

    [SerializeField]
    private GameObject Window;
    private RectTransform CanvasRect;
    //Which multiple to snap to on the desktop
    [SerializeField]
    private int gridX = 50;
    [SerializeField]
    private int gridY = 50;
    [SerializeField]
    private int offsetX = 150;
    [SerializeField]
    private int offsetY = 150;

    void Start()
    {
        icon = this.transform.gameObject;
        CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    private float roundToMultiple(float x, float multiple)
    {
        return Mathf.Round((x / multiple)) * multiple;
    }

    //Corners are bottom left, top left, top right, and bottom right respec.
    private bool InBounds(Vector3[] corners, Vector2 mousePos)
    {
        if(corners[0].x <= mousePos.x && corners[0].y <= mousePos.y &&
            corners[1].x <= mousePos.x && corners[1].y >= mousePos.y &&
            corners[2].x >= mousePos.x && corners[2].y >= mousePos.y &&
            corners[3].x >= mousePos.x && corners[3].y <= mousePos.y)
        {
            return true;
        }

        return false;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.clickCount == 2)
        {
            //Doesn't quite work; problems with scaling. But good for now.
            GameObject canvas = this.transform.parent.gameObject;
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
            GameObject newWindow = Instantiate(Window, center, Quaternion.identity);
            newWindow.transform.SetParent(canvas.transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3[] corners = new Vector3[4];
        CanvasRect.GetWorldCorners(corners);
        if(InBounds(corners, eventData.position))
        {
            icon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3[] corners = new Vector3[4];
        CanvasRect.GetWorldCorners(corners);

        float leftX = corners[0].x; float rightX = corners[2].x;
        float botY = corners[0].y; float topY = corners[2].y;

        Vector2 currentPos = new Vector2(roundToMultiple(eventData.position.x, gridX),
                                         roundToMultiple(eventData.position.y, gridY));

        if(currentPos.x < leftX + offsetX) { currentPos.x = leftX + offsetX; }
        if(currentPos.x > rightX - offsetX) { currentPos.x = rightX - offsetX; }
        if(currentPos.y < botY + offsetY) { currentPos.y = botY + offsetY; }
        if(currentPos.y > topY - offsetY) { currentPos.y = topY - offsetY; }

        icon.transform.position = currentPos;
    }

}
