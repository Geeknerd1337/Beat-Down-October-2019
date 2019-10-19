using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float forwardForce;
    public float upwardForce;
    public GameObject explosion;
    public GameObject muzzle;

    public float dmg;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = Instantiate(muzzle);
        g.transform.rotation = transform.parent.rotation;
        g.transform.position = transform.position;
        GetComponent<Rigidbody>().AddForce(transform.parent.forward * forwardForce);

        Destroy(g, 2f);

        transform.SetParent(null);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SongManager.ManagerInstance.beatFull && SongManager.ManagerInstance.beatCount3 == 1)
        {
            GameObject g = Instantiate(explosion);
            g.transform.position = transform.position;
            AreaDamageEnemies(transform.position, range, dmg);
            Destroy(g, 5f);
            Destroy(gameObject);
        }
    }


    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            Target enemy = col.GetComponent<Target>();
            if (enemy != null)
            {
                // linear falloff of effect
                float proximity = (location - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / radius);


                enemy.TakeDamage(damage * effect);
            }
        }

    }
}
