using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{

    [Header("Enemy Management")]
    public List<Transform> spawnPoints;
    public List<GameObject> spawnedEnemies;
    public List<Wave> waves;

    [Header("Time Management")]
    public float timer;
    public float timePerSpawn;

    [Header("Wave Management")]
    public float wave;
    public float timeBetweenWaves;
    private int index;
    private int spawnIndex;
    public bool started = false;
    private bool cr_running = false;
    private bool ended = false;


    [Header("Force Fields")]
    public GameObject force1;
    public GameObject force2;
    // Start is called before the first frame update
    void Start()
    {
        force1.SetActive(false);
        force2.SetActive(false);
        index = 0;
        spawnIndex = 0;

    }


    // Update is called once per frame
    void Update()
    {
     
        if(started && WaveOver())
        {
            index++;
            if (index >= waves.Count)
            {
                
                if (!ended)
                {
                    force1.SetActive(false);
                    force2.SetActive(false);
                    FindObjectOfType<WaveManager>().EndWaveMusic();
                    ended = true;
                }
            }
            else
            {
                spawnedEnemies = new List<GameObject>();
                StartCoroutine("SpawnEnemies");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!started && other.tag == "Player")
        {
            started = true;
            force1.SetActive(true);
            force2.SetActive(true);
            StartCoroutine("SpawnEnemies");
            FindObjectOfType<WaveManager>().StartWaveMusic();
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(timePerSpawn);
            cr_running = true;
            Transform t = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Wave w = waves[index];

            GameObject g = Instantiate(w.enemies[spawnIndex]);
            g.transform.position = t.position;
            g.GetComponent<SpawnEnemy>().myManager = this;
            spawnIndex++;



            if (spawnIndex == w.enemies.Count)
            {
                spawnIndex = 0;
                cr_running = false;
                yield break;

            }
        }
        

    }

    bool WaveOver()
    {


        bool b = true;
        for(int i = 0; i < spawnedEnemies.Count; i++)
        {
            if(spawnedEnemies[i] != null)
            {
                b = false;
            }
        }

        if(spawnedEnemies.Count == 0)
        {
            b = false;
        }


        if (cr_running)
        {
            b = false;
        }
        return b;
    }
}
