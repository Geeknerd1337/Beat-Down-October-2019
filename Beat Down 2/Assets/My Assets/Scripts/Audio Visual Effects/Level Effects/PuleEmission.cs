using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuleEmission : MonoBehaviour
{

    public AudioPeer peer;
    private Renderer r;
    public Color color1;
    public Color color2;
    public Vector2 minMaxValue;
    public int band;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        r.material.SetColor("_EmissionColor", Color.Lerp(color1, color2, peer._bandBuffer[band]));
    }
}
