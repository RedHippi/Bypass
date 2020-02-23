using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWindowScript : WindowScript
{

    void Start()
    {
    }

    public override void ReadInput(GameObject g)
    {
        if (g.name == "Mini") { IconParent.GetComponent<TextIconScript>().ToggleMin(); } //Run minimize on IconParent
        if (g.name == "Close") { IconParent.GetComponent<TextIconScript>().CloseWindow(); }
    }
}