﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Date
{
    public enum Month { January, February, March, April, May, June, July, August, September, October, November, December};
    public int day;
    public Month month;
    public int year;

    public string ProduceDate()
    {
        return ((int)month + 1).ToString() + "/" + day.ToString() + "/" + year.ToString();
    }
}

[Serializable]
public class Visited
{
    public string url;
    [TextArea]
    public string displayedText;
    public Date dateVisited;

    //Icon?
}

public class HistoryManager : MonoBehaviour
{
    public List<Visited> pastSites;
    public TMPro.TextMeshProUGUI textPrefab;
    public GameObject linkPrefab;
    private GameObject historySite;
    private VerticalLayoutGroup verticalLayout;
    public BrowserManager browser;
    public TMPro.TMP_InputField searchBar;

    [Header("Formatting")]
    public int linkSize;
    public int headerSize;
    //Fonts?

    private void Start()
    {
        historySite = this.transform.gameObject;
        verticalLayout = historySite.GetComponent<VerticalLayoutGroup>();
        pastSites.Sort((s1, s2) => CompareDates(s1.dateVisited, s2.dateVisited));
        CreateLinks();
    }
    
    private void CreateHeader(Date date)
    {
        TMPro.TextMeshProUGUI header = Instantiate(textPrefab) as TMPro.TextMeshProUGUI;
        header.text = date.ProduceDate();
        header.fontSize = headerSize;
        header.transform.SetParent(historySite.transform, false);
    }

    private void CreateLinks()
    {
        Date currentDate = pastSites[0].dateVisited; //Most recent
        CreateHeader(currentDate);

        for (int i = 0; i < pastSites.Count; i++)
        {
            if(CompareDates(pastSites[i].dateVisited, currentDate) != 0)
            {
                //Make some blank space
                TMPro.TextMeshProUGUI blank = Instantiate(textPrefab) as TMPro.TextMeshProUGUI;
                blank.transform.SetParent(historySite.transform, false);
                blank.text = " ";
                currentDate = pastSites[i].dateVisited;
                CreateHeader(currentDate);
            }

            GameObject link = Instantiate(linkPrefab);
            link.transform.SetParent(historySite.transform);

            int tmp = i; //Otherwise, i will be out of range inside the listener
            link.GetComponent<Button>().onClick.AddListener(() =>
            {
                searchBar.text = pastSites[tmp].url;
                browser.CheckURL(pastSites[tmp].url);
            });

            TMPro.TextMeshProUGUI linkText = link.GetComponent<TMPro.TextMeshProUGUI>();

            linkText.fontSize = linkSize;
            linkText.text = pastSites[i].displayedText;
            link.transform.localScale = Vector3.one;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayout.GetComponent<RectTransform>());
    }

    private int CompareDates(Date x, Date y)
    {
        if (x.year > y.year) { return -1; }
        else if (x.year < y.year) { return 1; }

        if ((int)x.month > (int)y.month) { return -1; }
        else if ((int)x.month < (int)y.month) { return 1; }

        if (x.day > y.day) { return -1; }
        else if (x.day < y.day) { return 1; }

        return 0;
    }

    //TODO: Add padding from the left, looks ugly as is.
    //TODO: Icons?
}
