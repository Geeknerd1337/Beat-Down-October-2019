using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SongManager : MonoBehaviour
{


    #region Singleton
    public static SongManager ManagerInstance;
    public void Awake()
    {

            if (ManagerInstance != null && ManagerInstance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                ManagerInstance = this;
                DontDestroyOnLoad(this.gameObject);
            }
    }
    #endregion

    //This gets song position
    private float songPosition;

    //Song position in beats
    private float songPosInBeats;

    //Song position in 1/8th beats
    private float songPosInBeatsD8;

    //Length of the beats
    private float secPerBeat;

    //Something to keep track of the songs position
    private float dspTimeSong;

    //The BPM for the tracks in the game
    public float BPM;

    //Number of beats currently played
    public float beatCount;
    public float beatCount2;
    public float beatCountD8;

    //If the beat is full;
    public bool beatFull = false;
    public bool beatFullD8 = false;


    //Currently I want to keep track of the beats and measure if the player is tapping with the beat

    //Margin of error that player has to press a note
    public float judgmentTime;


    //beatsAhead to spawn the note timer
    public float beatsAhead;

    //The timer object
    public GameObject indicator;
    public GameObject indicatorInstance;



    //Everything else below this line is just debug stuff until I give it an express purpose
    public Image hit;

    //Instance of the indicator
    
    public GameObject canvas;


    public AudioSource a;
    public List<AudioSource> audioList;
    private float time;

    void Start()
    {

        //Calculate the number of seconds in a beat
        secPerBeat = 60f / BPM;

        //record the time when the song starts
        dspTimeSong = (float)AudioSettings.dspTime;

        //start the song
        a.Play();
        foreach(AudioSource audio in audioList)
        {
            audio.Play();
        }




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
            songPosInBeatsD8 = songPosition / (secPerBeat / 4);


            //Set beat full to false;
            beatFull = false;
            //Uptick the beats when necessary
            if (beatCount < songPosInBeats)
            {
            beatFull = true;
                beatCount++;
                indicatorInstance = Instantiate(indicator, canvas.transform);
            indicatorInstance.transform.position = hit.transform.position;

            }

            //Set beat full D8 to be false
            beatFullD8 = false;
            if(beatCountD8 < songPosInBeatsD8)
            {
                beatCountD8++;
                beatCount2++;
                if(beatCount2 > 7)
                {
                    beatCount2 = 0;
                }
                beatFullD8 = true;
 
            }



            UpdateHitColor();


        if (Input.GetMouseButtonDown(0))
        {
            CheckIfValidTime();
        }


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


    public bool CheckIfValidTime(){
        float time = songPosition / secPerBeat;
        float targetBeat = Mathf.Round(time);
        //Debug.Log(time);
        //Debug.Log(beatCount - 1);
        //Debug.Log(targetBeat);
        if(time > (targetBeat) - judgmentTime && time < (targetBeat) + judgmentTime)
        {
            Debug.Log("True");
            Color TC = hit.color;
            TC.a = 1;
            hit.color = TC;
            return true;
        }
        else
        {
            Debug.Log("False");
            return false;
        }

    }
    
        
    


}
