using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffMap : MonoBehaviour
{

    public Transform respawnPoint;
    public ParticleSystem p;
    public AudioSource a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = respawnPoint.position;
            other.GetComponent<CharacterController>().enabled = true;
            p.Play();
            a.Play();
            FindObjectOfType<Player>().DamageD(10f);
        }
    }
}
