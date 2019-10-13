using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignNormalRaycast : MonoBehaviour
{
    Vector3 normalTarget;
    Quaternion targetRot;
    public float speed = 1f;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
                Vector3.down, out hit, 5f)){
            normalTarget = hit.normal;
        }
        /*
        Quaternion targetRotation = Quaternion.LookRotation(
            towardObjectFromHead, 
            transform.up
        );
        */
        targetRot = Quaternion.LookRotation(normalTarget,transform.forward);//transform.TransformDirection(Vector3.forward));
        //targetRot.eulerAngles = normalTarget;
        Debug.DrawLine(hit.point,hit.point+hit.normal*2f,Color.red);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            targetRot, 
            1 - Mathf.Exp(-speed * Time.deltaTime)
        );
    }
}
