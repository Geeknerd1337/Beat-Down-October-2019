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
    float ballSpeed = 1.0f;
    Rigidbody rb;
    Transform player;
    bool launched = false;

    public float targetScale;
    float curScale = 0.1f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform.localScale = Vector3.one * curScale ;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }


    void Update()
    {
        curScale += Time.deltaTime * 6f;
        curScale = Mathf.Clamp(curScale, 0, targetScale);
        transform.localScale = Vector3.one * curScale;

        if(transform.localScale == (Vector3.one * targetScale))
        {
            transform.SetParent(null);
            launchBall = true;
        }
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
                /*Vector3 targRot = abdomen.forward;
                Quaternion targetRotation = Quaternion.LookRotation(
                    targRot,
                    abdomen.up
                );*/
                /*
                abdomen.rotation = Quaternion.Slerp(
                    abdomen.rotation,
                    targetRotation,
                    1 - Mathf.Exp(-1 * Time.deltaTime)
                );
                /*/
                //abdomen.rotation = targetRotation;
                //*/
            }
            if (launchBall && !launched)
            {
                rb.AddForce(Vector3.right * 1000f);
                ballHome = player;
                launched = true;
            }
        }
        else
        {
            
        }
        if (!isClose)
        {
            //rotate butt down when ball isn't close
            /*Quaternion targetRotation = Quaternion.LookRotation(
                abdomen.forward,
                abdomen.up
            );
            abdomen.rotation = Quaternion.Slerp(
                abdomen.rotation,
                targetRotation,
                1 - Mathf.Exp(-1 * Time.deltaTime)
            );*/
        }
                

    }
}