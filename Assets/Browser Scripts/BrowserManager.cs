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
    public GameObject site;
    [Tooltip("Left value is the minimum time, right is the maximum.")]
    public bool displayQuick = false;
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
    [Tooltip("For non-default sites; that is, for sites that aren't error pages or home pages.")]

    [Header("Site Stuff")]
    private List<Site> sites;
    [SerializeField]
    private GameObject loadingIcon;

    private Dictionary<string, Site> sitesDic;
    private ScrollRect scrollComp;
    private GameObject currentlyActive; //Should always be only one
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
        GameObject obj = site.site; //holy shit
        if(site.displayQuick) { DisplayPageQuick(obj); yield break; }
        displaying = true;
        float loadTime = UnityEngine.Random.Range(site.totalLoadingRange.x, site.totalLoadingRange.y);
        float totalTime = 0;
        int currentIndex = 0;

        loadingIcon.SetActive(true);
        DisplayPageQuick(obj); //Same set-up, so why not?
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

    private void DisplayPageQuick(GameObject obj)
    {
        if (currentlyActive) {
            currentlyActive.GetComponent<RectTransform>().anchoredPosition = contentStartPosition;
            currentlyActive.SetActive(false);
        }
        obj.SetActive(true);
        currentlyActive = obj;
        scrollComp.content = obj.GetComponent<RectTransform>();
        contentStartPosition = obj.GetComponent<RectTransform>().anchoredPosition;
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
