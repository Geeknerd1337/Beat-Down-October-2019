﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescreaseSize : MonoBehaviour
{
    private float oldScale = 1.59f;
    private float newScale = 3f;
    private float time;
    public Color c;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = c;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(new Vector3(newScale, newScale, newScale), new Vector3(oldScale, oldScale, oldScale), time / 0.48f);
        time += Time.deltaTime;


        if(transform.localScale == new Vector3(oldScale, oldScale, oldScale))
        {
            Destroy(this.gameObject);
        }
    }
}
