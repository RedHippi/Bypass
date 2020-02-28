using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;

    private bool CanJump;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Move Left
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        //Move Right
        if (Input.GetKey(KeyCode.D)) { /*Move Right*/}

        //Jump
        if (Input.GetKey(KeyCode.Space) && CanJump) { /*Do a full routine */}
    }
    
    void FixedUpdate()
    {
    }
}
