using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnBeat : MonoBehaviour
{
    public SoundManager soundManager;
    public AudioClip tap;
    public AudioClip kick;
    public AudioSource player;
    private bool start;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BPM.beatFull)
        {
            soundManager.PlaySound(tap, 0.4f);
            KickStart();
            
            //To keep things accurate, over long periods of time, the beat will have to be resynchronized when the clip finishes, hopefully it doesn't get too distracting
            if (BPM.beatFull && BPM.beatCountFull % 64 == 0)
            {
                player.timeSamples = 0;
            }
            //Use if(BPM.beatfull % 2 == 0) to make something happen every other beat.
        }
    }

    void KickStart()
    {
        if (!start)
        {
            player.Play();
            start = true;
        }
    }
}
