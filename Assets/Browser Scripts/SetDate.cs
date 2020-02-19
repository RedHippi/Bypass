using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDate : MonoBehaviour
{
    public Date currentDate;
    [SerializeField]
    private int minYear;
    [SerializeField]
    private int maxYear;
    [SerializeField]
    private TMPro.TextMeshProUGUI dateDisplay;


    private TMPro.TMP_Dropdown monthDrop;
    private TMPro.TMP_Dropdown dayDrop;
    private TMPro.TMP_Dropdown yearDrop;

    void Awake()
    {
        monthDrop = this.transform.GetChild(0).GetComponent<TMPro.TMP_Dropdown>();
        dayDrop = this.transform.GetChild(1).GetComponent<TMPro.TMP_Dropdown>();
        yearDrop = this.transform.GetChild(2).GetComponent<TMPro.TMP_Dropdown>();

        List<string> years = new List<string>();

        for(int i = minYear; i < maxYear; i++)
        {
            years.Add(i.ToString());
        }

        currentDate.month = Date.Month.January;
        currentDate.day = 22;
        currentDate.year = 2006;
        dateDisplay.text = currentDate.ProduceDate();

        yearDrop.AddOptions(years);
        this.transform.localScale = Vector3.zero;
    }

    private Date.Month ConvertMonth(string str)
    {
        switch (str)
        {
            case "January":
                return Date.Month.January;
            case "February":
                return Date.Month.February;
            case "March":
                return Date.Month.March;
            case "April":
                return Date.Month.April;
            case "May":
                return Date.Month.May;
            case "June":
                return Date.Month.June;
            case "July":
                return Date.Month.July;
            case "August":
                return Date.Month.August;
            case "September":
                return Date.Month.September;
            case "October":
                return Date.Month.October;
            case "November":
                return Date.Month.November;
            case "December":
                return Date.Month.December;
            default:
                //Error...
                return Date.Month.December;
        }
    }

    private bool ValidDate(Date date)
    {
        List<Date.Month> list1 = new List<Date.Month>();
        List<Date.Month> list2 = new List<Date.Month>();

        list1.Add(Date.Month.January);list1.Add(Date.Month.March);
        list1.Add(Date.Month.May); list1.Add(Date.Month.July);
        list1.Add(Date.Month.August); list1.Add(Date.Month.October);
        list1.Add(Date.Month.December);

        list2.Add(Date.Month.April); list2.Add(Date.Month.June);
        list2.Add(Date.Month.September); list2.Add(Date.Month.November);

        if(list1.Contains(date.month)) { return true; }
        if(list2.Contains(date.month) && date.day <= 30) { return true; }

        bool leapYear = (date.year % 4 == 0 && (date.year % 100 != 0 || date.year % 400 == 0));

        if(date.day <= 28 || (leapYear && date.day <= 29)) { return true; }

        return false;
    }

    public void ChangeCurrentDate()
    {
        Date date = new Date();

        date.month = ConvertMonth(monthDrop.options[monthDrop.value].text);
        date.day = int.Parse(dayDrop.options[dayDrop.value].text);
        date.year = int.Parse(yearDrop.options[yearDrop.value].text);

        if (ValidDate(date))
        {
            currentDate = date;
            dateDisplay.text = date.ProduceDate();
        }
    }

    public void ToggleVisibility()
    {
        if(this.transform.localScale == Vector3.one)
        {
            this.transform.localScale = Vector3.zero;
        } else
        {
            this.transform.localScale = Vector3.one;
        }
    }
}
