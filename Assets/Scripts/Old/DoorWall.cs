using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWall : MonoBehaviour
{
    [SerializeField]
    public GameObject Wall;
    public GameObject Door;

    public Transform WallT { get; set; }
    public Transform DoorT { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        WallT = Wall.transform;
        DoorT = Door.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
