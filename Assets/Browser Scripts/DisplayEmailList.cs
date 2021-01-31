using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class User
{
    public string username;
    public string password;
    public List<Email> emails;
}


public class DisplayEmailList : MonoBehaviour
{
    //For testing only; switch to using the Resource folder later
    public List<User> users;
    [SerializeField]
    private int listSize = 7;
    [SerializeField]
    private GameObject headerPrefab;

    [HideInInspector]
    public bool displaySearch;
    [HideInInspector]
    public List<Email> currentEmails;
    [HideInInspector]
    public List<int> queriedEmails;

    [Header("Sub-pages")]
    public GameObject listEmails;
    public GameObject listEmail;
    public GameObject login;
    public GameObject loginFail;

    private bool failed = false;
    private GameObject emailsPanel;
    private GameObject emailContents;
    private TMPro.TextMeshProUGUI pageText;
    private int numPages;
    private int currentPage = 1; //Starts at 1
    private BrowserManager browser;
    private User currentUser = null;

    void Awake()
    {
        pageText = listEmails.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>();
        emailsPanel = listEmails.transform.GetChild(3).gameObject;
        emailContents = listEmail.transform.GetChild(1).gameObject;
        browser = FindObjectOfType<BrowserManager>();
    }

    public void UpdateEmailPanel()
    {
        RemoveChildren();
        numPages = (displaySearch) ? Mathf.CeilToInt((float)queriedEmails.Count / listSize) 
                                   : Mathf.CeilToInt((float)currentEmails.Count / listSize);
        numPages = (numPages <= 0) ? 1 : numPages;

        for (int i = (currentPage - 1) * listSize; i < currentPage * listSize; i++)
        {
            Email current;
            int index;

            if (displaySearch)
            {
                if(i >= queriedEmails.Count) { break; }
                index = queriedEmails[i]; current = currentEmails[index];
            } else if(i >= currentEmails.Count) { break; }
            else { current = currentEmails[i]; index = i; }

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
                browser.SwitchPage(listEmail);
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

    public void DisplayEmail(int emailID)
    {
        //emailContents must be active here
        TMPro.TextMeshProUGUI sender = emailContents.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI subject = emailContents.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI date = emailContents.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI body = emailContents.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();

        sender.text = "Sender: " + currentEmails[emailID].senderName + " [" + currentEmails[emailID].senderEmail + "]";
        subject.text = "Subject: " + currentEmails[emailID].subject;
        date.text = "-------------------- " + currentEmails[emailID].dateSent.ProduceDate() + "--------------------";
        body.text = currentEmails[emailID].body;
    }


    public void CheckLogin()
    {
        TMPro.TMP_InputField userField;
        TMPro.TMP_InputField passField;
        int index = (failed) ? 2 : 1;
        GameObject parent = (failed) ? loginFail : login;
        userField = parent.transform.GetChild(index).GetComponent<TMPro.TMP_InputField>();
        passField = parent.transform.GetChild(index+1).GetComponent<TMPro.TMP_InputField>();
        string username = userField.text; string password = passField.text;
        userField.text = ""; passField.text = "";

        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].username == username && users[i].password == password)
            {
                failed = false;
                currentUser = users[i];
                currentEmails = users[i].emails;
                currentEmails.Sort((s1, s2) => s1.dateSent.CompareDates(s2.dateSent));
                numPages = Mathf.CeilToInt((float)currentEmails.Count / listSize);
                UpdateEmailPanel();
                browser.SwitchPage(listEmails);
                return;
            }
        }

        failed = true;
        browser.SwitchPage(loginFail);
    }

    //TODO: Account for longish emails (scrollRect?)
}
