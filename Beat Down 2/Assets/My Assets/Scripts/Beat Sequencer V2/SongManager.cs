using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongManager : MonoBehaviour
{
    private float songPosition;

    private float songPosInBeats;

    private float secPerBeat;

    private float dspTimeSong;

    public float BPM;

    public float beatCount;

    public Image hit;
    public GameObject indicator;
    //Instance of the indicator
    public GameObject indicatorInstance;
    public GameObject canvas;


    public AudioSource a;
    public AudioSource b;
    private float time;

    void Start()
    {

        //Calculate the number of seconds in a beat
        secPerBeat = 60f / BPM;

        //record the time when the song starts
        dspTimeSong = (float)AudioSettings.dspTime;

        //start the song
        a.Play();
        b.Play();




        Color tc = hit.color;
        tc.a = 0;
        hit.color = tc;
    }

    void Update()
    {
        time += Time.deltaTime;


            //calculate the position in seconds
            songPosition = (float)(AudioSettings.dspTime - dspTimeSong);



            //calculate the position in beats
            songPosInBeats = songPosition / secPerBeat;


            if (beatCount < songPosInBeats)
            {


                beatCount++;




                Color tc = hit.color;
                tc.a = 1;
                hit.color = tc;
            }

            UpdateHitColor();
        }



    void UpdateHitColor()
    {
        Color tc = hit.color;
        if (tc.a > 0)
        {
            tc.a -= Time.deltaTime * (1/secPerBeat) * 2;
        }
        hit.color = tc;
    }


}
