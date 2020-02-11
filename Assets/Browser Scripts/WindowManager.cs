using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void DestroyAll()
    {
        Destroy(this.transform.gameObject);
    }

    public void Minimize()
    {
        //TODO
    }
}
