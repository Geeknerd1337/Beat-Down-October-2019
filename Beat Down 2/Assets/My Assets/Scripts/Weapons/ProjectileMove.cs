using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "player")
        {
            if (collision.transform != null)
            {
                speed = 0;
                Destroy(gameObject, 4f);
                gameObject.transform.SetParent(collision.transform);
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<Collider>());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
