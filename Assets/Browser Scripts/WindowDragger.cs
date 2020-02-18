using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDragger : MonoBehaviour
                         , IDragHandler
                         , IEndDragHandler
{

    private GameObject parentWindow;
    private Vector2 difference;
    private bool dragging = false;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        parentWindow = this.transform.parent.gameObject;
        difference = parentWindow.transform.position - this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging)
        {
            dragging = true;
            //Left/right offset
            offset = (Vector2)this.transform.position - eventData.pressPosition;
        }
        parentWindow.transform.position = eventData.position + difference + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
    }

    //TODO: Add "priority" on click; clicking on a window brings it to the front of the screen
}
