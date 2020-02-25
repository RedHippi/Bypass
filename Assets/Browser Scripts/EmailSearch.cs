using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailSearch : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown filterDropdown;
    [SerializeField]
    private TMPro.TextMeshProUGUI placeHolderText;
    private TMPro.TMP_InputField searchBox;
    private DisplayEmailList emailSite; //Need to traverse through emails

    void Start()
    {
        emailSite = this.transform.parent.parent.GetComponent<DisplayEmailList>();
        searchBox = this.transform.GetComponent<TMPro.TMP_InputField>();

        ChangePlaceHolder();
    }

    private bool CheckDateFormat(string test, out Date date)
    {
        int month; int day; int year;
        date = new Date();
        string[] split = test.Trim(' ').Split('/');

        if(split.Length != 3) { return false; }
        if(split[0].Length != 2 || !int.TryParse(split[0], out month)) { return false; }
        if(split[1].Length != 2 || !int.TryParse(split[1], out day)) { return false; }
        if(split[2].Length != 4 || !int.TryParse(split[2], out year)) { return false; }
        if(month < 1 || month > 12) { return false; }

        date.day = day;
        date.month = (Date.Month)(month - 1);
        date.year = year;

        return date.ValidDate();
    }

    private bool CheckNameFormat(string name)
    {
        return true;
    }

    private bool CheckEmailFormat(string address)
    {
        string[] split1 = address.Trim(' ').Split('@');
        if(split1.Length != 2) { return false; }
        string[] split2 = split1[1].Trim(' ').Split('.');
        if (split2.Length < 2) { return false; }

        return true;
    }

    //For now, the query can be max one word
    public void SearchEmails(string query)
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }
        if (query == "") { emailSite.displaySearch = false; emailSite.UpdateEmailPanel(); return; }
        string filter = filterDropdown.options[filterDropdown.value].text;
        emailSite.displaySearch = true;
        emailSite.queriedEmails.Clear();
        Date date = new Date();

        switch (filter)
        {
            case "Names":
                if(!CheckNameFormat(query)) { emailSite.UpdateEmailPanel(); return; }
                break;

            case "Emails":
                if (!CheckEmailFormat(query)) { emailSite.UpdateEmailPanel(); return; }
                break;

            case "Dates":
                if(!CheckDateFormat(query, out date)) { emailSite.UpdateEmailPanel(); return; }
                break;
        }

        for (int i = 0; i < emailSite.emails.Count; i++)
        {
            switch (filter)
            {
                case "Names":
                    if (emailSite.emails[i].senderName.Contains(query))
                    {
                        emailSite.queriedEmails.Add(i);
                    }
                    break;

                case "Emails":
                    if (emailSite.emails[i].senderEmail.Contains(query))
                    {
                        emailSite.queriedEmails.Add(i);
                    }
                    break;

                case "Dates":
                    if (date.CompareDates(emailSite.emails[i].dateSent) == 0)
                    {
                        emailSite.queriedEmails.Add(i);
                    }
                    break;
            }
        }

        emailSite.UpdateEmailPanel();
    }

    public void ChangePlaceHolder()
    {
        string filter = filterDropdown.options[filterDropdown.value].text;
        searchBox.text = "";

        switch (filter)
        {
            case "Names":
                placeHolderText.text = "Search by name...";
                break;

            case "Emails":
                placeHolderText.text = "Search by email...";
                break;

            case "Dates":
                placeHolderText.text = "MM/DD/YYYY";
                break;
        }
    }

}
