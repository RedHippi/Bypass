using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;


[Serializable]
public class PageLoadInfo
{
    public bool displayQuick = false;
    [Tooltip("Left value is the minimum time, right is the maximum.")]
    public Vector2 totalLoadingRange = new Vector2(1, 4);
    public string pageURL;
    [Tooltip("Should this page be accessible if the user types the address in the URL?")]
    public bool publicAccess = true;
    //TODO: Add "cache" so the website doesn't take forever to load every time.
}


[Serializable]
public class Site
{
    [TextArea]
    public string historyText = "Default description!";
    public GameObject site;
    [Tooltip("Set to -1 if this website only has one page to load. Otherwise, set to the index of the starting page.")]
    public List<PageLoadInfo> subpagesInfo;
}

public class BrowserManager : MonoBehaviour
{
    [Header("Important Pages")]
    [SerializeField]
    private GameObject blankPage;
    [SerializeField]
    private GameObject homePage;
    [SerializeField]
    private GameObject error404;
    [SerializeField]
    private HistoryManager history;
    public TMPro.TMP_InputField searchBar;

    [Header("Site Stuff")]
    [SerializeField]
    private List<Site> sites;
    [SerializeField]
    private GameObject loadingIcon;

    private Dictionary<string, Site> sitesDic;
    private Dictionary<string, int> pageIndex;
    private ScrollRect scrollComp;
    private GameObject currentlyActivePage; //Should always be only one
    private Site currentlyActiveSite = null;
    private int currentlyActiveIndex;
    private Vector2 contentStartPosition;
    private bool displaying = false;
    private Coroutine co;

    void Start()
    {
        scrollComp = this.transform.GetComponent<ScrollRect>();
        sitesDic = new Dictionary<string, Site>();
        pageIndex = new Dictionary<string, int>();

        for (int i = 0; i < sites.Count; i++)
        {
            for(int j = 0; j < sites[i].subpagesInfo.Count; j++)
            {
                if (sites[i].subpagesInfo[j].publicAccess)
                {
                    sitesDic.Add(sites[i].subpagesInfo[j].pageURL, sites[i]);
                    pageIndex.Add(sites[i].subpagesInfo[j].pageURL, j);
                }
            }
            if(sites[i].subpagesInfo.Count != 1) { DeactivateChildren(sites[i].site); }
            sites[i].site.SetActive(false);
        }

        blankPage.SetActive(false);
        error404.SetActive(false);
        loadingIcon.SetActive(false);
        DisplayPageQuick(homePage);

    }

    //I want the load time per element on a website to be slightly random, so
    //this function generates a wait period per element. 
    private void GenerateRandomSplit(float target, float[] output)
    {
        float total = 0;
        for(int i = 0; i < output.Length; i++)
        {
            output[i] = UnityEngine.Random.Range(0.0f, 1.0f);
            total += output[i];
        }

        //Scaling
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = (output[i] / total) * target;
            if(i != 0) { output[i] += output[i - 1]; }
        }
    }

    private void DeactivateChildren(GameObject obj)
    {
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayPage(Site site, int index = 0)
    {
        GameObject obj = site.site;
        if(site.subpagesInfo[index].displayQuick) { DisplayPageQuick(obj, site, index); yield break; }
        history.UpdateHistory(site, index);
        displaying = true;
        float loadTime = UnityEngine.Random.Range(site.subpagesInfo[index].totalLoadingRange.x, 
                                                  site.subpagesInfo[index].totalLoadingRange.y);
        float totalTime = 0;
        int currentIndex = 0;

        loadingIcon.SetActive(true);
        obj = DisplayPageQuick(obj, site, index); //Same set-up, so why not?
        float[] waitArr = new float[obj.transform.childCount]; //child count > 0
        DeactivateChildren(obj);
        GenerateRandomSplit(loadTime, waitArr);

        while (totalTime < loadTime)
        {
            totalTime += Time.deltaTime;
            if(totalTime > waitArr[currentIndex])
            {
                obj.transform.GetChild(currentIndex).gameObject.SetActive(true);
                currentIndex++;
            }
            yield return null;
        }

        displaying = false;
        loadingIcon.SetActive(false);
        yield break;
    }

    //I had to change this function to account for possibly multiple pages per website.
    private GameObject DisplayPageQuick(GameObject obj, Site site = null, int index = 0)
    {
        if (currentlyActivePage) {
            currentlyActivePage.GetComponent<RectTransform>().anchoredPosition = contentStartPosition;
            currentlyActivePage.SetActive(false);
            if(currentlyActiveSite != null && currentlyActiveSite.site != null) { currentlyActiveSite.site.SetActive(false); }
            currentlyActiveIndex = -1;
        }

        //The count check is for webpages that have no children that are also webpages
        if (site != null && site.subpagesInfo.Count != 1)
        {
            currentlyActiveSite = site;
            site.site.SetActive(true);
            obj = site.site.transform.GetChild(index).gameObject; //By convention, set the starting page at index 0
        }

        obj.SetActive(true);
        currentlyActivePage = obj;
        scrollComp.content = obj.GetComponent<RectTransform>();
        contentStartPosition = obj.GetComponent<RectTransform>().anchoredPosition;
        currentlyActiveIndex = index;
        if(site != null && site.subpagesInfo[index].pageURL != "")
        {
            searchBar.text = site.subpagesInfo[index].pageURL;
        }
        return obj;
    }

    public void SwitchPage(GameObject newsite)
    {
        if(currentlyActiveSite == null || currentlyActiveSite.site == null) { return; }
        if (currentlyActiveSite.subpagesInfo.Count == 1 || currentlyActivePage == null) { return; }
        int index = newsite.transform.GetSiblingIndex();
        if(currentlyActiveSite.subpagesInfo.Count < index + 1) { return; }

        if (!displaying)
        {
            co = StartCoroutine(DisplayPage(currentlyActiveSite, index));
        }
        else
        {
            displaying = false;
            StopCoroutine(co);
            loadingIcon.SetActive(false);
            co = StartCoroutine(DisplayPage(currentlyActiveSite, index));
        }
    }

    //Use this function when calling this function from stuff that's
    //not the address bar.
    public void CheckURL(string url)
    {
        //TODO: Strip out www at the start of the string
        url = url.ToLower();
        if (!displaying)
        {
            if (sitesDic.ContainsKey(url))
            {
                int index = pageIndex[url];
                co = StartCoroutine(DisplayPage(sitesDic[url], index));
            }
            //TODO: Make special page for forbidden access
            else 
            {
                DisplayPageQuick(error404);
            }
        } else
        {
            displaying = false;
            StopCoroutine(co);
            loadingIcon.SetActive(false);
            CheckURL(url);
        }
    }

    //The Unity inspector only accepts functions with <= 1 parameter, so I 
    //have to do this stupid hack now...
    public void EnterPressed(string url)
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }
        CheckURL(url);
    }
}
