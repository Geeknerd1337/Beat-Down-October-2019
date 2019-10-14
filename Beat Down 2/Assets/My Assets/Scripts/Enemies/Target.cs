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
    }

    // Update is called once per frame
    void Update()
    {
        healthValue = Mathf.Lerp(healthValue, Mathf.Lerp(-0.1f, 0.18f, 1 - (health / maxHealth)), 0.3f);
        r.material.SetFloat("_Amount", healthValue);
    }



    public void TakeDamage(float amt)
    {
        health -= amt;

        if (health <= 0f)
        {
            Die();
        }
    }


    void Die()
    {
        partSystem.transform.SetParent(null);
        partSystem.GetComponent<ParticleSystem>().Play();
        Destroy(partSystem, 6f);
        Destroy(parent);
        WaveManager.WaveManagerInstance.enemyCount--;

    }
}
