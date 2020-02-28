using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance;

 public AudioClip hit;

    private AudioSource player;

    private void Awake()
    {
        player = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayHitTest()
    {
        PlaySFX(hit, 1f);
    }


    public void PlaySFX(AudioClip clip, float volume = 1f)
    { 
        player.PlayOneShot(clip, volume);
    }
}
