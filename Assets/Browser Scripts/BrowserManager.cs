using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[Serializable]
public class URLPair
{
    public string url;
    public GameObject site;
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
    private List<URLPair> sites;
    [SerializeField]
    [Tooltip("Left value is the minimum time, right is the maximum.")]
    private Vector2 totalLoadingRange;
    [SerializeField]
    private GameObject loadingIcon;

    private Dictionary<string, GameObject> sitesDic;
    private ScrollRect scrollComp;
    private GameObject currentlyActive; //Should always be only one
    private float minPageLoadingTime;
    private float maxPageLoadingTime;
    private Vector2 contentStartPosition;

    void Start()
    {
        minPageLoadingTime = totalLoadingRange.x;
        maxPageLoadingTime = totalLoadingRange.y;
        Assert.IsTrue(minPageLoadingTime < maxPageLoadingTime);
        Assert.IsTrue(minPageLoadingTime >= 0 && maxPageLoadingTime >= 0);

        scrollComp = this.transform.GetComponent<ScrollRect>();
        sitesDic = new Dictionary<string, GameObject>();

        for(int i = 0; i < sites.Count; i++)
        {
            sitesDic.Add(sites[i].url, sites[i].site);
            sites[i].site.SetActive(false);
        }

        blankPage.SetActive(false);
        error404.SetActive(false);
        loadingIcon.SetActive(false);
        DisplayPageQuick(homePage);
    }

    //I want the load time per element on a website to be random, so
    //this function generates a wait period per element. 
    void GenerateRandomSplit(float target, float[] output)
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

    void DeactivateChildren(GameObject obj)
    {
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayPage(GameObject obj)
    {
        float loadTime = UnityEngine.Random.Range(minPageLoadingTime, maxPageLoadingTime);
        float totalTime = 0;
        int currentIndex = 0;
        loadingIcon.SetActive(true);
        obj.SetActive(true);
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

        loadingIcon.SetActive(false);
        if (currentlyActive)
        {
            currentlyActive.GetComponent<RectTransform>().anchoredPosition = contentStartPosition;
            currentlyActive.SetActive(false);
        }
        currentlyActive = obj;
        scrollComp.content = obj.GetComponent<RectTransform>();
        contentStartPosition = obj.GetComponent<RectTransform>().anchoredPosition;
        yield break;
    }

    void DisplayPageQuick(GameObject obj)
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
        if (sitesDic.ContainsKey(url))
        {
            DisplayPageQuick(blankPage);
            StartCoroutine(DisplayPage(sitesDic[url]));
        } else
        {
            DisplayPageQuick(error404);
        }
    }
}
