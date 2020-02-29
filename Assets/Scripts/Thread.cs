using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Post
{
    public string name; //Will be anonymized automatically
    public Date datePosted;
    public string reply; //Set to empty if not a reply
    [TextArea(4, 10)]
    public string message;
}

[CreateAssetMenu]
public class Thread : ScriptableObject
{
    [TextArea(4, 5)]
    public string boardTitle;
    [TextArea(4, 5)]
    public string threadTitle;
    public List<Post> posts;
}
