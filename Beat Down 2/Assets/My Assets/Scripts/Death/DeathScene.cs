using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DeathScene : MonoBehaviour
{

    public PostProcessVolume volume;
    public ChromaticAberration chromaticAberration;
    private float deathTimer;
    public float deathTime;
    public AudioSource[] a;

    // Start is called before the first frame update
    void Start()
    {
        volume = FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out chromaticAberration);
        chromaticAberration.intensity.value = 5f;
        a = GetComponentsInChildren<AudioSource>();
        foreach(AudioSource sound in a)
        {
            sound.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer < deathTime)
        {
            deathTimer += Time.deltaTime;
        }

        chromaticAberration.intensity.value = Mathf.Lerp(0, 5f, 1 - (deathTimer / deathTime));
    }
}
