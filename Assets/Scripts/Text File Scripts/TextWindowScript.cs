using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWindowScript : WindowManager
{
    public TextAsset contentText;
    private string textFilePath;
    private TMP_Text contentTMP;
    
    void Start()
    {
        textFilePath = "Text Files/" + MyName.Substring(0, MyName.Length - 4);
        contentText = Resources.Load<TextAsset>(textFilePath);
        contentTMP = transform.Find("Body/Viewport/Content").GetComponent<TMP_Text>();
        contentTMP.text = contentText.text;

        RectTransform contentRT = transform.Find("Body/Viewport/Content").GetComponent<RectTransform>();
        contentRT.sizeDelta = new Vector2(contentTMP.preferredWidth * 0.75f, contentTMP.preferredHeight + 10);
    }
}