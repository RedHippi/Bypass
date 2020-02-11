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
    //Which multiple to snap to on the desktop
    [SerializeField]
    private int gridX = 50;
    [SerializeField]
    private int gridY = 50;

    void Start()
    {
        icon = this.transform.gameObject;
    }

    private float roundToMultiple(float x, float multiple)
    {
        return Mathf.Round((x / multiple)) * multiple;
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
        icon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.transform.position = new Vector3(roundToMultiple(eventData.position.x, gridX),
                                            roundToMultiple(eventData.position.y, gridY));
    }
}
