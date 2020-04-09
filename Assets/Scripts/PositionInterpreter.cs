using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInterpreter : MonoBehaviour
{

    //This script reads information from our PercentArr object and applies it to
    //the position of the attached object.

    [SerializeField]
    private GameObject border;
    public PercentArr Pos; //[x,y]

    private Bounds ScreenBounds;
    private float minX, minY, maxX, maxY, width, height;

    // Start is called before the first frame update
    void Start()
    {
        ScreenBounds = border.GetComponent<SpriteRenderer>().bounds;
        (minX, maxX) = (ScreenBounds.min.x, ScreenBounds.max.x);
        (minY, maxY) = (ScreenBounds.min.y, ScreenBounds.max.y);
        (width, height) = (maxX - minX, maxY - minY);
    }

    // Update is called once per frame
    void Update()
    {
        float xN = width * Pos.array[0];
        float yN = height * Pos.array[1];
        gameObject.transform.position = new Vector3(minX + xN, minY + yN,-10f);
    }
}
