using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointInfo : MonoBehaviour
{

    public GameObject Held;
    public GameObject Over;
    public GameObject Left;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Held != null && !Held.GetComponent<TaskbarScript>().IsHeld) Held.transform.position = transform.position;
        if (Held == null && Left != null && !Left.GetComponent<TaskbarScript>().IsHeld)
        {
            Held = Left;
            Left = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Held == null)
        {
            Held = collision.gameObject;
        }
        else if (Held != collision.gameObject)
        {
            Over = collision.gameObject;
        }
    }
    //TODO Update the OnExit to have it where if the object leaves but doesn't go somewhere new, toggle back to being on.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Equals(collision.gameObject, Held))
        {
            Left = Held;
            Held = null;
        }
        
    }
}
