using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatJudgeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckValid();
    }

    void CheckValid()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(BPM.BPMInstance.WithinJudgeTimeNote());
        }
    }
}
