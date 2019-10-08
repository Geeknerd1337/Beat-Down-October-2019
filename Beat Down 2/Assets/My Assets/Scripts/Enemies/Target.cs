using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
    private float maxHealth;
    private Renderer r;
    private float healthValue;


    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        r.material = new Material(r.material);
        maxHealth = health;
        healthValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthValue = Mathf.Lerp(healthValue, Mathf.Lerp(0, 0.144f, 1 - (health / maxHealth)), 0.3f);
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
        Destroy(gameObject);
    }
}
