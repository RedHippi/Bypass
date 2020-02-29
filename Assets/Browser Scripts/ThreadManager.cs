using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
    [SerializeField]
    private Thread thread;
    [SerializeField]
    private TMPro.TextMeshProUGUI threadTitle;
    [SerializeField]
    private TMPro.TextMeshProUGUI boardTitle;
    [SerializeField]
    private GameObject postPrefab;
    [SerializeField]
    private GameObject threadPanel;
    [SerializeField]
    private Color32 replyColor;
    [Range(8, 16)]
    public int messagePadding = 8;
    private int setHeight = 0;

    void Awake()
    {
        threadTitle.text = thread.threadTitle;
        boardTitle.text = thread.boardTitle;

        for (int i = 0; i < thread.posts.Count; i++)
        {
            SetPostValues(i);
        }
    }

    void SetPostValues(int index)
    {
        Post post = thread.posts[index];
        GameObject obj = Instantiate(postPrefab);

        TMPro.TextMeshProUGUI IDDate = obj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI response = obj.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        IDDate.text = Anonymizer.FetchID(post.name) + ", " + post.datePosted.ProduceDate();
        if(post.reply != "")
        {
            response.text = "<color=#" + ColorUtility.ToHtmlStringRGBA((replyColor)) 
                            + ">" + ">>>> Replying to: " + Anonymizer.FetchID(post.reply) + "</color>\n";
        }
        response.text += post.message;

        obj.transform.SetParent(threadPanel.transform);
    }

    IEnumerator SetHeight()
    {
        yield return null;

        RectTransform threadRect = threadPanel.GetComponent<RectTransform>();
        RectTransform thisRect = this.GetComponent<RectTransform>();
        float offset = this.transform.position.y - threadPanel.transform.position.y;
        float heightDifference = threadRect.rect.height - (thisRect.rect.height - offset);

        if (heightDifference > 0)
        {
            thisRect.sizeDelta = new Vector2(thisRect.sizeDelta.x, thisRect.sizeDelta.y + heightDifference);
        }

        yield break;
    }

    //Explanation for hack: 
    //https://forum.unity.com/threads/solved-cant-get-the-rect-width-rect-height-of-an-element-when-using-layouts.377953/
    private void OnEnable()
    {
        setHeight++;
        if(setHeight <= 2)
        {
            StartCoroutine("SetHeight");
        }
    }

    //TODO: Bug where if "Display Quick" is set to false, the page won't properly resize. 
    //This happens because all the children are deactivated, so the height can't be fetched.

    //Solution: Switch from deactivating to toggling scale
}
