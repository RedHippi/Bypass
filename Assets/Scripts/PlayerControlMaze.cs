using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMaze : MonoBehaviour
{
    public float speed = 5f;

    private bool CanJump;
    private Rigidbody2D _body;
    private Vector3 _inputs = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        Vector3 current = GetComponent<Rigidbody>().position;

        GetComponent<Rigidbody>().position = new Vector3(horz * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0) + current;
        */

        // Calculate how fast we should be moving
        _inputs   = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.y = Input.GetAxis("Vertical");

        /*
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;
        */
        /*
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = 0;
        GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
        */
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
        _body.transform.position += _inputs * Time.deltaTime * speed;
    }
}
