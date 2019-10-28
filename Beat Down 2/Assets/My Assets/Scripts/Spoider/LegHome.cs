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
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2f,//transform.TransformDirection(Vector3.up) * 2f,
                Vector3.down,//transform.TransformDirection(Vector3.down), 
                out hit, 5f)){
            transform.position = hit.point;
        }
        //transform.rotation = Quaternion.LookRotation(Vector3.forward,Vector3.up);
        Debug.DrawLine(transform.position + Vector3.up * 2f, transform.position + Vector3.up * 2f + Vector3.down * 5f);

    }
}
