using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CLScript : MonoBehaviour
{
    // Start is called before the first frame update
    public struct oneChat {
        public bool firstPerson;
        public string message;
    }
    private string firstUsername;
    private string secondUsername;
    private string totalChat;
    //private oneChat[] allChats = new oneChat[1];
    
    public GameObject chatObject;
    public Transform parent;
    public TextAsset textStuff;

    private void Awake()
    {
        string oneMessage = "";
        string[] lines = textStuff.text.Split("\n"[0]);
        firstUsername = lines[0];
        secondUsername = lines[1];
        for (int i = 2; i < lines.Length; i++)
        {
            string[] chatMessage = lines[i].Split(":"[0]);
            if (chatMessage[0].Equals("1"))
            {
                oneMessage = "<color=blue>[" + firstUsername + "]</color>:" + chatMessage[1];
            }
            else
            {
                oneMessage = "<color=red>[" + secondUsername + "]</color>:" + chatMessage[1];
            }
            oneMessage = oneMessage.Replace("\r", "");
            GameObject g = Instantiate(chatObject, parent);
            g.GetComponentInChildren<TMP_Text>().SetText(oneMessage);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

}
