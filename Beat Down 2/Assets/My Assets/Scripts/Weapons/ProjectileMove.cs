using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public Transform creator;

    public bool hurtPlayer;

    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        if(muzzlePrefab != null)
        {
            GameObject muzzleVfx = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVfx.transform.forward = gameObject.transform.forward;
            Destroy(muzzleVfx, 5f);
            muzzleVfx.transform.SetParent(creator);
            muzzleVfx.transform.localPosition = Vector3.zero;
            //muzzleVfx.transform.localRotation = Quaternion.Euler(Vector3.zero);

        }
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

                if(hitPrefab != null)
                {
                    GameObject hitVFX = Instantiate(hitPrefab, pos, rot);
                    Destroy(hitVFX, 3f);
                }

                if(collision.gameObject.GetComponent<Target>() != null)
                {
                    collision.gameObject.GetComponent<Target>().TakeDamage(10f);
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
}
