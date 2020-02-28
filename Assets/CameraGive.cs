using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGive : MonoBehaviour
{
    public TexturePasser tp;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.GetComponent<Camera>());
        tp.cameras[index] = gameObject.GetComponent<Camera>();
    }
}
