using System;
using System.Collections.Generic;
using UnityEngine;

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
    public string displayedText;
    public Date dateVisited;
    //Icon?
}

public class HistoryManager : MonoBehaviour
{
    public List<Visited> pastSites;

    private void Start()
    {
        pastSites.Sort((s1, s2) => CompareDates(s1.dateVisited, s2.dateVisited));
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
}
