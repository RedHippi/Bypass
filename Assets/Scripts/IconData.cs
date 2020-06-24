using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconData : MonoBehaviour
{
    public TMP_Text text;
    public Image image;

    public void SetupIcon(Image newImage, string name)
    {
        image.sprite = newImage.sprite;
        text.text = name;
    }
}
