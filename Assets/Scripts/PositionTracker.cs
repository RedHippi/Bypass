using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    //This script reads position information from its object and sends it
    //to our PercentArr object.


    public BoundStorage bound;
    public PercentArr posPercent; // [x,y]

    private RectTransform windowRect;
    private Vector3 windowPos;
    private float minX, maxX, minY, maxY, width, height;

    // Start is called before the first frame update
    void Start()
    {
        windowRect = GetComponent<RectTransform>();
        (minX, maxX) = bound.Bound1;
        (minY, maxY) = bound.Bound2;
        (width, height) = (maxX - minX, maxY - minY);
    }

    private void Update()
    {
        windowPos = windowRect.position;
        posPercent.array[0] = (windowPos.x - minX) / width;
        posPercent.array[1] = (windowPos.y - minY) / height;
    }
}
