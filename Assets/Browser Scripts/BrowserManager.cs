using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[Serializable]
public class Site
{
    public string url;
    [Tooltip("If Display Quick is set to true, this site will not be added to the history page.")]
    [TextArea]
    public string historyText = "Default description!";
    public GameObject site;
    public bool displayQuick = false;
    [Tooltip("Set to -1 if this website only has one page to load. Otherwise, set to the index of the starting page.")]
    public int defaultPage = -1;
    [Tooltip("Left value is the minimum time, right is the maximum.")]
    public Vector2 totalLoadingRange = new Vector2(1,4);
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

    [Header("Site Stuff")]
    [SerializeField]
    private List<Site> sites;
    [SerializeField]
    private GameObject loadingIcon;

    private Dictionary<string, Site> sitesDic;
    private ScrollRect scrollComp;
    private GameObject currentlyActivePage; //Should always be only one
    private Site currentlyActiveSite = null; 
    private Vector2 contentStartPosition;
    private bool displaying = false;
    private Coroutine co;

    void Start()
    {
        scrollComp = this.transform.GetComponent<ScrollRect>();
        sitesDic = new Dictionary<string, Site>();

        for(int i = 0; i < sites.Count; i++)
        {
            sitesDic.Add(sites[i].url, sites[i]);
            if(sites[i].defaultPage != -1) { DeactivateChildren(sites[i].site); }
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

    IEnumerator DisplayPage(Site site)
    {
        GameObject obj = site.site;
        if(site.displayQuick) { DisplayPageQuick(obj, site); yield break; }
        history.UpdateHistory(site);
        displaying = true;
        float loadTime = UnityEngine.Random.Range(site.totalLoadingRange.x, site.totalLoadingRange.y);
        float totalTime = 0;
        int currentIndex = 0;

        loadingIcon.SetActive(true);
        obj = DisplayPageQuick(obj, site); //Same set-up, so why not?
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
    private GameObject DisplayPageQuick(GameObject obj, Site site = null)
    {
        if (currentlyActivePage) {
            currentlyActivePage.GetComponent<RectTransform>().anchoredPosition = contentStartPosition;
            currentlyActivePage.SetActive(false);
            if(currentlyActiveSite != null && currentlyActiveSite.site != null) { currentlyActiveSite.site.SetActive(false); }
        }

        if (site != null && site.defaultPage != -1)
        {
            currentlyActiveSite = site;
            site.site.SetActive(true);
            obj = site.site.transform.GetChild(site.defaultPage).gameObject;
        }

        obj.SetActive(true);
        currentlyActivePage = obj;
        scrollComp.content = obj.GetComponent<RectTransform>();
        contentStartPosition = obj.GetComponent<RectTransform>().anchoredPosition;
        return obj;
    }

    public void CheckURL(string url)
    {
        if (!displaying)
        {
            if (sitesDic.ContainsKey(url))
            {
                co = StartCoroutine(DisplayPage(sitesDic[url]));
            }
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
}
