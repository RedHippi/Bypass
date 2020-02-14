using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
    private float minLoadingTime = 0;
    [SerializeField]
    private float maxLoadingTime = 0;
    [SerializeField]
    private GameObject loadingIcon;

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

        blankPage.SetActive(false);
        error404.SetActive(false);
        loadingIcon.SetActive(false);
        DisplayPageQuick(homePage);
    }

    IEnumerator DisplayPage(GameObject obj)
    {
        float loadTime = UnityEngine.Random.Range(minLoadingTime, maxLoadingTime);
        float passedTime = 0;
        loadingIcon.SetActive(true);

        while (passedTime < loadTime)
        {
            passedTime += Time.deltaTime;
            yield return null;
        }

        loadingIcon.SetActive(false);
        if (currentlyActive) { currentlyActive.SetActive(false); }
        obj.SetActive(true);
        currentlyActive = obj;
        scrollComp.content = obj.GetComponent<RectTransform>();

        yield break;
    }

    void DisplayPageQuick(GameObject obj)
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
            DisplayPageQuick(blankPage);
            StartCoroutine(DisplayPage(sitesDic[url]));
        } else
        {
            DisplayPageQuick(error404);
        }
    }

    //IDEAS: Add a loading bar and some buffer "loading time" to make it look better?
    //IDEAS: Make the page load in chunks, like back in the day
}
