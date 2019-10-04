using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{


    #region Singleton
    public static BPM BPMInstance;
    private void Awake()
    {
        if (BPMInstance != null && BPMInstance != this) {
            Destroy(this.gameObject);
        }
        else
        {
            BPMInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public float bpm;
    private float beatInterval, beatTimer, beatIntervalD8, beatTimerD8;
    public static bool beatFull, beatD8;
    public static int beatCountFull, beatCountD8;
    public AudioSource songCheck;
    public float judgeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BeatDetection();
    }


    void BeatDetection()
    {
        //full beat count
        beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;
        if(beatTimer >= beatInterval)
        {
            beatTimer -= beatTimer;
            beatFull = true;
            beatCountFull++;
            //Debug.Log("Beat");
            //Debug.Log(songCheck.timeSamples);
        }
        //Divided beat count
        beatD8 = false;
        beatIntervalD8 = beatInterval / 4;
        beatTimerD8 += Time.deltaTime;
        if(beatTimerD8 >= beatIntervalD8)
        {
            beatTimerD8 -= beatIntervalD8;
            beatD8 = true;
            beatCountD8++;
        }
    }

    public bool WithinJudgeTimeBar()
    {
        if(beatTimer >= (beatInterval - judgeTime))
        { 
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool WithinJudgeTimeNote()
    {
        if (beatTimerD8 >= (beatIntervalD8 - judgeTime))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetInterval()
    {
        return beatInterval;
    }
}
