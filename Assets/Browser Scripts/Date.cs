using System;
using System.Collections.Generic;

[Serializable]
public struct Date
{
    public enum Month { January, February, March, April, May, June, July, August, September, October, November, December };
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
