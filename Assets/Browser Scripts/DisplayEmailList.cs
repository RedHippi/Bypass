using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEmailList : MonoBehaviour
{
    //For testing only; switch to using the Resource folder later
    public List<Email> emails;
    [SerializeField]
    private int listSize = 7;
    [SerializeField]
    private GameObject headerPrefab;
    public bool displaySearch;
    public List<int> queriedEmails;

    private GameObject pageOne;
    private GameObject pageTwo;
    private GameObject emailsPanel;
    private GameObject emailContents;
    private TMPro.TextMeshProUGUI pageText;
    private int currentSite = 1; //1 or 2
    private int numPages;
    private int currentPage = 1; //Starts at 1

    void Awake()
    {
        pageOne = this.transform.GetChild(0).gameObject;
        pageTwo = this.transform.GetChild(1).gameObject;
        pageText = pageOne.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>();
        emailsPanel = pageOne.transform.GetChild(3).gameObject;
        emailContents = pageTwo.transform.GetChild(1).gameObject;
        emails.Sort((s1, s2) => s1.dateSent.CompareDates(s2.dateSent));
        numPages = Mathf.CeilToInt((float) emails.Count / listSize);

        UpdateEmailPanel();
    }

    public void UpdateEmailPanel()
    {
        RemoveChildren();

        for (int i = (currentPage - 1) * listSize; i < currentPage * listSize; i++)
        {
            Email current;
            int index;

            if (displaySearch)
            {
                if(i >= queriedEmails.Count) { break; }
                index = queriedEmails[i]; current = emails[index];
            } else if(i >= emails.Count) { break; }
            else { current = emails[i]; index = i; }

            GameObject temp = Instantiate(headerPrefab);
            temp.transform.SetParent(emailsPanel.transform);
            temp.transform.localScale = Vector3.one;

            TMPro.TextMeshProUGUI sender = temp.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            TMPro.TextMeshProUGUI subject = temp.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
            TMPro.TextMeshProUGUI date = temp.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();

            sender.text = current.senderName;
            subject.text = current.subject;
            date.text = current.dateSent.ProduceDate();

            int tmp = index;
            Button button = temp.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SwitchPage();
                DisplayEmail(tmp);
            });
        }

        pageText.text = "Page " + currentPage.ToString() + "/" + numPages.ToString();
    }

    void RemoveChildren()
    {
        int childCount = emailsPanel.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(emailsPanel.transform.GetChild(i).gameObject);
        }
    }

    public void FlipPage(int modifier)
    {
        currentPage += modifier;
        if(currentPage <= 0) { currentPage = 1; return; }
        if(currentPage > numPages) { currentPage = numPages; return; }
        UpdateEmailPanel();
    }

    private void FlipActive(GameObject webpage, bool value)
    {
        for(int i = 0; i < webpage.transform.childCount; i++)
        {
            webpage.transform.GetChild(i).gameObject.SetActive(value);
        }
        webpage.SetActive(value);
    }

    public void SwitchPage()
    {
        bool value = (currentSite == 1) ? false : true;
        currentSite = (currentSite == 1) ? 2 : 1;
        FlipActive(pageOne, value);
        FlipActive(pageTwo, !value);
    }

    public void DisplayEmail(int emailID)
    {
        //emailContents must be active here
        TMPro.TextMeshProUGUI sender = emailContents.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI subject = emailContents.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI date = emailContents.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI body = emailContents.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();

        sender.text = "Sender: " + emails[emailID].senderName + " [" + emails[emailID].senderEmail + "]";
        subject.text = "Subject: " + emails[emailID].subject;
        date.text = "-------------------- " + emails[emailID].dateSent.ProduceDate() + "--------------------";
        body.text = emails[emailID].body;
    }

    //TODO: Account for longish emails (scrollRect?)
}
