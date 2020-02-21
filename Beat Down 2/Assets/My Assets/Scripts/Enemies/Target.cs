using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
    private float maxHealth;
    public Renderer r;
    private float healthValue;
    public GameObject partSystem;
    public GameObject parent;
    public GameObject gem;
    private AudioSource deathSound;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Renderer>() != null)
        {
            r = GetComponent<Renderer>();
        }
        r.material = new Material(r.material);
        maxHealth = health;
        healthValue = 0;
        deathSound = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        healthValue = Mathf.Lerp(healthValue, Mathf.Lerp(-0.1f, 0.18f, 1 - (health / maxHealth)), 0.3f);
        r.material.SetFloat("_Amount", healthValue);
    }



    public void TakeDamage(float amt)
    {
        if (health > 0)
        {
            health -= amt;

            if (health <= 0f)
            {
                Die();
            }
        }
    }


    void Die()
    {
        deathSound.pitch = Random.Range(0.95f, 1.05f);
        deathSound.Play(); 
        deathSound.transform.SetParent(null);
        Destroy(deathSound.gameObject, 3f);
        int i = Random.Range(0, 4);
        for(int b = 0; b < i; b++)
        {
            GameObject g = Instantiate(gem);
            g.transform.SetParent(null);
            g.transform.position = transform.position;
        }
        partSystem.transform.SetParent(null);
        partSystem.GetComponent<ParticleSystem>().Play();
        Destroy(partSystem, 6f);
        Destroy(parent);
        WaveManager.WaveManagerInstance.enemyCount--;

    }
}
