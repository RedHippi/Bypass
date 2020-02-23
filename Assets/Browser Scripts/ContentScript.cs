using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScript : MonoBehaviour
{
    
    public GameObject Home;
    public GameObject Downloads;
    public GameObject Documents;
    public GameObject Desktop;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void toggleHome(){
        Home.SetActive (!Home.activeSelf);
    }
    public void toggleDownloads(){
        Downloads.SetActive (!Downloads.activeSelf);
    }
    public void toggleDocuments(){
        Documents.SetActive (!Documents.activeSelf);
    }
    public void toggleDesktop(){
        Desktop.SetActive (!Desktop.activeSelf);
    }
}
