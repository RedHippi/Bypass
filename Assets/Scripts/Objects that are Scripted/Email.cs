using UnityEngine;

[CreateAssetMenu]
public class Email : ScriptableObject
{
    public string senderEmail;
    public string senderName;
    public Date dateSent;
    public string subject;
    [TextArea(5, 10)]
    public string body;
}
