using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungBall : MonoBehaviour
{
    [SerializeField] Transform ballHome;
    [SerializeField] Transform abdomen;
    [SerializeField] Transform bumTarget;
    bool hasBall = true;
    bool launchBall = false;
    float ballSpeed = .5f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isClose = (Vector3.Distance(ballHome.position, transform.position) < 2);
        if (hasBall){
            //transform.position += (ballHome.position - transform.position)/ballSpeed;
            //transform.position += (new Vector3(ballHome.position.x,transform.position.y,ballHome.position.z)-transform.position) / ballSpeed;
            rb.AddForce((new Vector3(ballHome.position.x, transform.position.y, ballHome.position.z) - transform.position) / ballSpeed);
            if (isClose)
            {
                //rotate butt up when it has the ball

                //Vector3 targRot = bumTarget.rotation * (bumTarget.position - abdomen.position);
                Vector3 targRot = abdomen.forward;
                Quaternion targetRotation = Quaternion.LookRotation(
                    targRot,
                    abdomen.up
                );
                /*
                abdomen.rotation = Quaternion.Slerp(
                    abdomen.rotation,
                    targetRotation,
                    1 - Mathf.Exp(-1 * Time.deltaTime)
                );
                /*/
                abdomen.rotation = targetRotation;
                //*/
            }
            if (launchBall)
            {
                rb.AddForce(transform.eulerAngles,ForceMode.Impulse);
            }
        }
        else
        {
            
        }
        if (!isClose)
        {
            //rotate butt down when ball isn't close
            Quaternion targetRotation = Quaternion.LookRotation(
                abdomen.forward,
                abdomen.up
            );
            abdomen.rotation = Quaternion.Slerp(
                abdomen.rotation,
                targetRotation,
                1 - Mathf.Exp(-1 * Time.deltaTime)
            );
        }
                

    }
}