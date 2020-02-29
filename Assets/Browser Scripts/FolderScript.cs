using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FolderScript : MonoBehaviour
{
    [SerializeField]
    private bool isSelected;

    public GameObject MyContent;
    
    private bool isActive = false;

    private void Start()
    {
        isActive = false;
        MyContent.SetActive(isActive);
    }
    private void OnPointerClick(){//PointerEventData eventData){
        /*if(eventData.button == PointerEventData.InputButton.Left){
            Debug.Log("Hello");
            isActive = !isActive;
            MyContent.SetActive(isActive);
        }*/
    }
}
