using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawn : MonoBehaviour
{

    private Rigidbody rb;
    public float upWardForce;
    public float sideForce;
    public Player p;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float width = Random.Range(-1f, 1f) * sideForce;
        float height = Random.Range(-1f, 1f) * sideForce;

        Vector3 torque = new Vector3(0,0,0);
            torque.x = Random.Range(-200, 200);
            torque.y = Random.Range(-200, 200);
            torque.z = Random.Range(-200, 200);

            rb.AddForce(transform.up * upWardForce + transform.right * width + transform.forward * height);
        rb.AddTorque(torque);
        p = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SongManager.ManagerInstance.beatFull)
        {
            float width = Random.Range(-1f, 1f) * sideForce/5f;
            float height = Random.Range(-1f, 1f) * sideForce/5f;

            Vector3 torque = new Vector3(0, 0, 0);
            torque.x = Random.Range(-200, 200);
            torque.y = Random.Range(-200, 200);
            torque.z = Random.Range(-200, 200);

            rb.AddForce(transform.up * upWardForce/2f + transform.right * width + transform.forward * height);
            rb.AddTorque(torque);
        }

        if(Vector3.Distance(transform.position,p.transform.position) < 5f)
        {
            rb.AddForce((new Vector3(p.transform.position.x, p.transform.position.y, p.transform.position.z) - transform.position)/0.01f);
            Debug.Log("mater");
        }
    }



}
