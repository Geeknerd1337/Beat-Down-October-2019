using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyToSpawn;
    private Renderer r;
    public float startScale;
    public float endScale;
    private float scaleLerp;
    private float amt = 0f;
    private bool madeEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Renderer>() != null)
        {
            r = GetComponent<Renderer>();
        }
        r.material = new Material(r.material);
        transform.localScale = new Vector3(startScale, startScale, startScale);
        r.material.SetFloat("_Amount", amt);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x != endScale)
        {
            scaleLerp += Time.deltaTime;
            transform.localScale = Vector3.Lerp(new Vector3(startScale, startScale, startScale), new Vector3(endScale, endScale, endScale), scaleLerp);
        }
        else
        {
            if (!madeEnemy)
            {
                GameObject g = Instantiate(enemyToSpawn);
                g.transform.position = transform.position;
                madeEnemy = true;
            }
            amt += Time.deltaTime;
            r.material.SetFloat("_Amount", amt);
            if(amt >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
