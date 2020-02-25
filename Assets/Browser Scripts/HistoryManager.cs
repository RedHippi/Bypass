using System;
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

    //0 means the dates are equal. -1 means this is more recent.
    public int CompareDates(Date y)
    {
        if (this.year > y.year) { return -1; }
        else if (this.year < y.year) { return 1; }

        if ((int)this.month > (int)y.month) { return -1; }
        else if ((int)this.month < (int)y.month) { return 1; }

        if (this.day > y.day) { return -1; }
        else if (this.day < y.day) { return 1; }

        return 0;
    }

    public bool ValidDate()
    {
        List<Date.Month> list1 = new List<Date.Month>();
        List<Date.Month> list2 = new List<Date.Month>();

        list1.Add(Date.Month.January); list1.Add(Date.Month.March);
        list1.Add(Date.Month.May); list1.Add(Date.Month.July);
        list1.Add(Date.Month.August); list1.Add(Date.Month.October);
        list1.Add(Date.Month.December);

        list2.Add(Date.Month.April); list2.Add(Date.Month.June);
        list2.Add(Date.Month.September); list2.Add(Date.Month.November);

        if (list1.Contains(this.month)) { return true; }
        if (list2.Contains(this.month) && this.day <= 30) { return true; }

        bool leapYear = (this.year % 4 == 0 && (this.year % 100 != 0 || this.year % 400 == 0));

        if (this.day <= 28 || (leapYear && this.day <= 29)) { return true; }

        return false;
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
    public BrowserManager browser;
    public TMPro.TMP_InputField searchBar;

    [Header("Formatting")]
    public int linkSize;
    public int headerSize;
    //Fonts?

    private GameObject historySite;
    private VerticalLayoutGroup verticalLayout;
    private ContentSizeFitter sizeFitter;

    private void Start()
    {
        historySite = this.transform.gameObject;
        verticalLayout = historySite.GetComponent<VerticalLayoutGroup>();
        sizeFitter = historySite.GetComponent<ContentSizeFitter>();

        //Should probably sort, then push to a stack. But I doubt this is a problem.
        pastSites.Sort((s1, s2) => s1.dateVisited.CompareDates(s2.dateVisited));
        CreateLinks();
    }
    
    private void InsertKeepSorted(Visited visit)
    {
        Date date = visit.dateVisited;

        for(int i = 0; i < pastSites.Count; i++)
        {
            if(date.CompareDates(pastSites[i].dateVisited) == -1)
            {
                pastSites.Insert(i, visit);
                return;
            }
        }

        pastSites.Add(visit); //Oldest date, so add it to the end.
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
            if(pastSites[i].dateVisited.CompareDates(currentDate) != 0)
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

    void RemoveChildren()
    {
        int childCount = historySite.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(historySite.transform.GetChild(i).gameObject);
        }
    }

    public void UpdateHistory(Site site)
    {
        Visited visit = new Visited();
        Date current = FindObjectOfType<SetDate>().currentDate;
        visit.url = site.url;
        visit.displayedText = site.historyText;
        visit.dateVisited = current;

        InsertKeepSorted(visit);
        RemoveChildren();
        CreateLinks();
    }

    //TODO: Add padding from the left, looks ugly as is.
    //TODO: Icons?
}
