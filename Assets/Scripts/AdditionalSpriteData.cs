using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalSpriteData : MonoBehaviour
{
    public float X;
    public float Y;


    public void LoadBounds()
    {
        SpriteRenderer mine = gameObject.GetComponent<SpriteRenderer>();
        Bounds tmp = mine.bounds;
        tmp.size = new Vector3(X,Y, 0f);
    }

    public void PrintBounds()
    {
        Vector3 tmp = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        Debug.Log($"{gameObject.name}'s bounds are [X:{tmp.x}|Y:{tmp.y}]");
    }
}
