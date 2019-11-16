using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySingleSound : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
        a.clip = sound;
        a.Play();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
