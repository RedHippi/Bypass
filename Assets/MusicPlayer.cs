using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private AudioSource player;
    private bool is_paused;
    private bool from_start;

    // Start is called before the first frame update
    void Start()
    {
        player.loop = false;
        is_paused = false;
        from_start = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset the audio player if it reaches the end
        if (!is_paused && !player.isPlaying && !from_start)
        {
            from_start = true;
        }
    }

    void SetTrack(AudioClip track)
    {
        player.Stop();
        player.clip = track;
    }

    void ToggleLoop()
    {
        player.loop = !player.loop;
    }

    void Play()
    {
        if (player.clip != null)
        {
            if (from_start)
            {
                from_start = false;
                is_paused = false;
                player.Play();
            }
            else
            {
                if (is_paused)
                {
                    player.UnPause();
                    is_paused = false;
                }
                else
                {
                    player.Pause();
                    is_paused = true;
                }
            }

        }
    }

    void Stop() 
    {
        if (player.clip != null)
        {
            from_start = true;
            player.Stop();
        }
    }

    void SetVolume(float vol)
    {
        player.volume = Mathf.Max(0f, Mathf.Min(1f, vol));
    }

}
