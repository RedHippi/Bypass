using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryPressed : MonoBehaviour
{

    public GameObject MyParent;
    public GameObject Selected;

    
    private void OnMouseDown()
    {
        MyParent.GetComponent<WindowScript>().ReadInput(gameObject);
    }
    
    private void OnMouseOver(){//hovering
        Selected.SetActive(true);   
    }

    private void OnMouseExit(){//no hover
        Selected.SetActive(false);
    }    
}
