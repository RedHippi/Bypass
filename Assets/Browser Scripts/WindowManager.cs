using UnityEngine;
using UnityEngine.EventSystems;

public class WindowManager : MonoBehaviour
                           , IPointerClickHandler
{

    //TODO: Switch to using TaskbarManager to focus
    public virtual void OnPointerClick(PointerEventData eventData)
    {

        this.transform.SetAsLastSibling();
    }
}
