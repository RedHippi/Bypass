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

    // Start is called before the first frame update
    void Start()
    {
        parentWindow = this.transform.parent.gameObject;
        difference = parentWindow.transform.position - this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        parentWindow.transform.position = eventData.position + difference;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //No snapping
    }

    //TODO: Add "priority" on click; clicking on a window brings it to the front of the screen
}
