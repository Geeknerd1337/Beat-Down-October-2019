using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatJudgeTest : MonoBehaviour
{
    private bool trigNextBeat;

    public Image hit;
    public GameObject indicator;
    //Instance of the indicator
    public GameObject indicatorInstance;
    public GameObject canvas;
    public bool hold;

    public Color validColor;


    // Start is called before the first frame update
    void Start()
    {
        trigNextBeat = false;
        Color tc = hit.color;
        tc.a = 0;
        hit.color = tc;
    }

    // Update is called once per frame
    void Update()
    {
        CheckValid();
        UpdateHitColor();
    }

    void CheckValid()
    {
        //Just an example of a weapon that can be held or has to be timed
        if (!hold)
        {
            //Checks if we are within the judgement time.
            if (indicatorInstance != null)
            {
                    if (Input.GetMouseButtonDown(0) && !trigNextBeat && BPM.BPMInstance.WithinJudgeTimeNote() && indicatorInstance.GetComponent<Image>().color == validColor)
                    {
                        trigNextBeat = true;
                    }
            }


            //Trigger an event if the next beat is full
            if (trigNextBeat && BPM.beatFull)
            {
                trigNextBeat = false;
                Debug.Log("test");
                Color tc = hit.color;
                tc.a = 1;
                hit.color = tc;
            }
        }
        else
        {
            //Trigger an event if the beat is full when we're holding the mouse
            if (Input.GetMouseButton(0) && BPM.beatFull)
            {
                trigNextBeat = false;
                Debug.Log("test");
                Color tc = hit.color;
                tc.a = 1;
                hit.color = tc;
            }
        }


        if (BPM.beatFull)
        {
            GameObject g =  Instantiate(indicator, canvas.transform);
            g.transform.localPosition = hit.transform.localPosition;
            if(BPM.beatCountFull % 2 == 0)
            {
                g.GetComponent<Image>().color = validColor;
            }
            indicatorInstance = g;

        }
    }

    void UpdateHitColor()
    {
        Color tc = hit.color;
        if(tc.a > 0)
        {
            tc.a -= Time.deltaTime * (1/BPM.BPMInstance.GetInterval());
        }
        hit.color = tc;
    }
}
