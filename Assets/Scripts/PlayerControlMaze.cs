using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMaze : MonoBehaviour
{
    public float speed = 5f;
    public AudioClip collideAudio;

    private bool CanJump;
    private Rigidbody2D _body;
    private Vector3 _inputs = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = collideAudio;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate how fast we should be moving
        _inputs   = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.y = Input.GetAxis("Vertical");

        //Jump
        if (Input.GetKey(KeyCode.Space) && CanJump) { /*Do a full routine */}
    }
    
    void FixedUpdate()
    {
        _body.transform.position += _inputs * Time.deltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)  //Plays sound whenever collision detected
    {
        GetComponent<AudioSource>().Play();
    }
}
