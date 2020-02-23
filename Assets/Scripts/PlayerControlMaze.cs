using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMaze : MonoBehaviour
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
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        transform.Translate(horz * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0);
        /*
        //Move Left
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        //Move Right
        if (Input.GetKey(KeyCode.D)) { /*Move Right} */

        //Jump
        if (Input.GetKey(KeyCode.Space) && CanJump) { /*Do a full routine */}
    }
    
    void FixedUpdate()
    {
    }
}
