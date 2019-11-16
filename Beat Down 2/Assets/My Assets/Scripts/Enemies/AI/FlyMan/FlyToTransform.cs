using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToTransform : MonoBehaviour
{


    public Transform target;
    private Rigidbody rb;
    public float speed;

    public GameObject explosion;

    public float diveDistance;
    public float explosionDistance;
    private Player p;

    public float dmg;
    public float range;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.SetParent(null);
        p = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTargetWithRotation(target, 1f, speed);

        if(Vector3.Distance(transform.position,p.transform.position) < diveDistance)
        {
            target = p.transform;
            speed = 20f;
        }


        if (Vector3.Distance(transform.position, p.transform.position) < explosionDistance)
        {
            ParticleSystem[] ps = explosion.GetComponentsInChildren<ParticleSystem>();
            ps[0].Play();
            ps[1].Play();
            explosion.transform.SetParent(null);
            AreaDamageEnemies(transform.position, range, dmg);
            Destroy(explosion, 2f);
            Destroy(parent);
            Destroy(gameObject);



        }



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

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, diveDistance);
    }

    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            Player enemy = col.GetComponent<Player>();
            if (enemy != null)
            {
                // linear falloff of effect
                float proximity = (location - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / radius);


                enemy.DamageD(damage * effect);
            }
        }

    }
}
