using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    public List<AudioClip> sounds;
    private AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
        a.clip = sounds[Random.Range(0, sounds.Count)];
        a.Play();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
