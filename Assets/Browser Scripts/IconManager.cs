using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class IconManager : MonoBehaviour
                         , IPointerClickHandler
                         , IDragHandler
                         , IEndDragHandler
{
    public TaskbarManager taskbar;
    [SerializeField]
    private GameObject windowPrefab;

    private bool selected = false;
    private GameObject icon;
    private IconLayoutManager layout;
    private Vector2 initialPos;
    private bool dragging = false;

    //[HideInInspector]
    public Sprite MyImage;
    //[HideInInspector]
    public string MyName;

    void Awake()
    {
        MyImage = GetComponentInChildren<Image>().sprite;
        MyName = GetComponentInChildren<TMP_Text>().text;
    }

    void Start()
    {
        icon = this.transform.gameObject;
        layout = this.transform.parent.GetComponent<IconLayoutManager>();
        //Assert.IsNotNull(layout);
    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //TODO: Highlight if one click.

        if (eventData.clickCount == 2)
        {
            //Doesn't quite work; problems with scaling. But good for now.
            GameObject canvas = this.transform.parent.gameObject;
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
            taskbar.CreateWindow(windowPrefab, center, MyName);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //To reset to a known position in case "something" happens
        if(!dragging) { dragging = true; initialPos = this.transform.position; }
        if(layout.InBounds(eventData.position))
        {
            icon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        icon.transform.position = layout.CheckEndPosition(initialPos, eventData.position);
    }
}
