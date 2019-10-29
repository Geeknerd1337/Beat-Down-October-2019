using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToTransform : MonoBehaviour
{


    public Transform target;
    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        FollowTargetWithRotation(target, 1f, speed);
    }

    void FollowTargetWithRotation(Transform target, float distanceToStop, float speed)
    {
        if (Vector3.Distance(transform.position, target.position) > distanceToStop)
        {
            transform.LookAt(target);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        }
    }

    void FollowTargetWitouthRotation(Transform target, float distanceToStop, float speed)
    {
        var direction = Vector3.zero;
        if (Vector3.Distance(transform.position, target.position) > distanceToStop)
        {
            direction = target.position - transform.position;
            rb.AddRelativeForce(direction.normalized * speed, ForceMode.Force);
        }
    }
}
