using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTake : MonoBehaviour
{
    public AudioSource a;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Player>().money++;
            a.transform.SetParent(null);
            a.pitch = Random.Range(0.9f,1.1f);
            a.Play();
            Destroy(a.gameObject, 2f);
            Destroy(transform.parent.gameObject);
        }
    }
}
