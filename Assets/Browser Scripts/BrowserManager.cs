using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class URLPair
{
    public string url;
    public GameObject site;
}


public class BrowserManager : MonoBehaviour
{

    [SerializeField]
    private GameObject homePage;
    [SerializeField]
    private GameObject error404;
    [SerializeField]
    [Tooltip("For non-default sites; that is, for sites that aren't error pages or home pages.")]
    private List<URLPair> sites;
    private Dictionary<string, GameObject> sitesDic;
    private ScrollRect scrollComp;
    private GameObject currentlyActive; //Should always be only one

    void Start()
    {
        scrollComp = this.transform.GetComponent<ScrollRect>();
        sitesDic = new Dictionary<string, GameObject>();

        for(int i = 0; i < sites.Count; i++)
        {
            sitesDic.Add(sites[i].url, sites[i].site);
            sites[i].site.SetActive(false);
        }

        error404.SetActive(false);
        DisplayPage(homePage);
    }

    void DisplayPage(GameObject obj)
    {
        if (currentlyActive) { currentlyActive.SetActive(false); }
        obj.SetActive(true);
        currentlyActive = obj;
        scrollComp.content = obj.GetComponent<RectTransform>();
    }

    public void CheckURL(string url)
    {
        if (sitesDic.ContainsKey(url))
        {
            DisplayPage(sitesDic[url]);
        } else
        {
            DisplayPage(error404);
        }
    }

    //IDEAS: Add a loading bar and some buffer "loading time" to make it look better?
}
