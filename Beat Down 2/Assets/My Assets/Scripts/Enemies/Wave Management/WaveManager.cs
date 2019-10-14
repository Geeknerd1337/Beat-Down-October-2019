using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    #region Singleton
    public static WaveManager WaveManagerInstance;
    public void Awake()
    {

        if (WaveManagerInstance != null && WaveManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            WaveManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public List<Transform> spawnPoints;

    public List<GameObject> spawnedEnemies;


    private float timer;
    public float maxEnemyCount;
    public float enemyCount;
    public float timePerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }


    void SpawnEnemies()
    {
        timer += Time.deltaTime;
        if(timer > timePerSpawn && enemyCount < maxEnemyCount)
        {
            Transform t = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject g = Instantiate(spawnedEnemies[0]);
            g.transform.position = t.position;
            enemyCount++;
            timer = 0;
        }
    }
}
