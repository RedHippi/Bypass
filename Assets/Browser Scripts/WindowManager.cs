using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
                           , IPointerClickHandler
{
    [HideInInspector] public string MyName;
    private GameObject myIcon;

    [SerializeField] private Image image;
    [SerializeField] private TMP_Text Name;

    //TODO: Switch to using TaskbarManager to focus
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        this.transform.SetAsLastSibling();
    }

    public void SetName(string name)
    {
        MyName = name;
        Name.text = name;
    }

    public void SetIcon(GameObject game)
    {
        myIcon = game;
    }

    public void SetImage(Image set)
    {
        image.sprite = set.sprite;
    }



    public void InformOfDeath()
    {
        myIcon.GetComponent<IconManager>().windowOpened = false;
    }
}
