using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public float damage = 10f;

    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public Transform creator;

    public bool hurtPlayer;

    public bool stop;
    public bool piercing;
    public float life;
    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("Projectile Created");
        if(muzzlePrefab != null)
        {
            GameObject muzzleVfx = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVfx.transform.forward = gameObject.transform.forward;
            Destroy(muzzleVfx, 5f);
            muzzleVfx.transform.SetParent(creator);
            muzzleVfx.transform.localPosition = Vector3.zero;
            //muzzleVfx.transform.localRotation = Quaternion.Euler(Vector3.zero);

        }

        Destroy(gameObject, 10f);
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
        if (collision.gameObject.tag != "player" && !hurtPlayer)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (collision.transform != null)
            {
                if (!piercing)
                {
                    if (stop)
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
                else
                {
                    life -= Time.deltaTime;
                    if (life < 0)
                    {
                        Destroy(gameObject);
                    }
                        if (collision.gameObject.layer == 12)
                    {

                    }
                    else
                    {
                        if (stop)
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

                if(hitPrefab != null)
                {
                    GameObject hitVFX = Instantiate(hitPrefab, pos, rot);
                    Destroy(hitVFX, 3f);
                }

                if(collision.gameObject.GetComponent<Target>() != null)
                {
                    collision.gameObject.GetComponent<Target>().TakeDamage(damage);
                }

                


            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(collision.gameObject.tag == "Player" && hurtPlayer)
            {

                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;

                if (hitPrefab != null)
                {
                    GameObject hitVFX = Instantiate(hitPrefab, pos, rot);
                    Destroy(hitVFX, 3f);
                }
                Destroy(gameObject);

                if(collision.gameObject.GetComponent<Player>() != null)
                {
                    collision.gameObject.GetComponent<Player>().Damage();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "player" && !hurtPlayer && collision.gameObject.layer != 9 && collision.gameObject.layer != 8)
        {
            RaycastHit hit;
            Quaternion rot = Quaternion.Euler(Vector3.zero);
            Vector3 pos = transform.position;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                 rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                 pos = hit.point;
            }


            if (collision.transform != null)
            {
                if (!piercing)
                {
                    if (stop)
                    {
                        speed = 0;
                        Destroy(gameObject, 4f);
                        gameObject.transform.SetParent(collision.transform);
                        Destroy(GetComponent<Rigidbody>());
                        Destroy(GetComponent<Collider>());
                        Debug.Log(collision.name);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    life -= Time.deltaTime;
                    if (life < 0)
                    {
                        Destroy(gameObject);
                    }
                    if (collision.gameObject.layer == 12)
                    {

                    }
                    else
                    {
                        if (stop)
                        {
                            speed = 0;
                            Destroy(gameObject, 4f);
                            gameObject.transform.SetParent(collision.transform);
                            Destroy(GetComponent<Rigidbody>());
                            Destroy(GetComponent<Collider>());
                            Debug.Log(collision.gameObject.layer);
                        }
                        else
                        {
                            Destroy(gameObject);
                        }
                    }
                }

                if (hitPrefab != null)
                {
                    GameObject hitVFX = Instantiate(hitPrefab, pos, rot);
                    Destroy(hitVFX, 3f);
                }

                if (collision.gameObject.GetComponent<Target>() != null)
                {
                    collision.gameObject.GetComponent<Target>().TakeDamage(damage);
                }




            }
            else
            {
                Destroy(gameObject);
            }
        }
       
    }
}
