using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSensitivity : MonoBehaviour
{
    public Slider s;
    public CameraMovement cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.sensitivity.x = Mathf.Lerp(1.5f, 10, s.value);
        cam.sensitivity.y = Mathf.Lerp(1.5f, 10, s.value);
    }
}
