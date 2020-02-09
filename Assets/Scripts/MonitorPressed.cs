using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorPressed : MonoBehaviour
{

    public GameObject MyParent;

    private void OnMouseDown()
    {
        MyParent.GetComponent<WindowScript>().ReadInput(gameObject);
    }
}
