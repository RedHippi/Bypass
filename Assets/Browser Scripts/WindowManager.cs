using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class WindowManager : MonoBehaviour
                           , IPointerClickHandler
{
    public string MyName;
    //TODO: Switch to using TaskbarManager to focus
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        this.transform.SetAsLastSibling();
    }

    public void SetName(string name)
    {
        MyName = name;
        transform.Find("Name").GetComponent<TMP_Text>().text = " " + name;
    }
}
