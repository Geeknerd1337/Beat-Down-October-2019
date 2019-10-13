using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegHome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
