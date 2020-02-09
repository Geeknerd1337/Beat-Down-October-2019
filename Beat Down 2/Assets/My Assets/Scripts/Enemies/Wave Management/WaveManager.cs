﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    [Header("Wave Control Actual")]
    public int wave;
    public float timeBetweenWaves;
    private float waveTimer;
    private int index;
    private bool canWait = false;

    [Header("Song Control")]
    public List<AudioSource> mainMelody;
    public List<AudioSource> breakDown;
    public AudioSource ambience;

    public GameObject waveTextPrefab;
    public GameObject UI;
    private bool textSpawned = false;


    // Start is called before the first frame update
    void Start()
    {
        foreach(AudioSource a in mainMelody)
        {
            a.volume = 0;
        }
        foreach (AudioSource a in breakDown)
        {
            a.volume = 0;
        }
        ambience.volume = 1;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    IEnumerator ChangeVolume(AudioSource a, float f)
    {

        while (a.volume != f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            a.volume = Approach(a.volume, f, Time.deltaTime/3f);
            if(a.volume == f)
            {
                yield break;
            }
        }
    }

    public void StartWaveMusic()
    {
        foreach (AudioSource a in mainMelody)
        {
            StartCoroutine(ChangeVolume(a, 1f));
        }
        StartCoroutine(ChangeVolume(ambience, 0));
    }

    public void EndWaveMusic()
    {
        foreach (AudioSource a in mainMelody)
        {
            StartCoroutine(ChangeVolume(a, 0f));
        }
        StartCoroutine(ChangeVolume(ambience, 1f));
    }

    void OldWaveInit()
    {
        if (!canWait)
        {
            //ooStartWave();

        }

        if (waveTimer == 0 && enemyCount > 0)
        {
            if ((SongManager.ManagerInstance.beatCount3 == 1) && breakDown[0].volume == 1 && SongManager.ManagerInstance.beatFull)
            {
                foreach (AudioSource a in breakDown)
                {
                    a.volume = 0;
                }
                foreach (AudioSource a in mainMelody)
                {
                    a.volume = 1;
                }
            }
        }

        if (canWait && enemyCount == 0)
        {
            waveTimer += Time.deltaTime;
            if ((SongManager.ManagerInstance.beatCount3 == 1) && breakDown[0].volume == 0 && SongManager.ManagerInstance.beatFull)
            {
                foreach (AudioSource a in breakDown)
                {
                    a.volume = 1;
                }
                foreach (AudioSource a in mainMelody)
                {
                    a.volume = 0;
                }
            }
            if (waveTimer > timeBetweenWaves)
            {
                waveTimer = 0;
                maxEnemyCount += 3;
                index = 0;
                canWait = false;


            }
        }
    }

    void StartWave()
    {
        timer += Time.deltaTime;
        if(timer > timePerSpawn && index < maxEnemyCount)
        {
            Transform t = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject g = Instantiate(spawnedEnemies[0]);
            g.transform.position = t.position;
            enemyCount++;
            timer = 0;
            index++;


            if (!textSpawned) {
                wave++;
                GameObject g2 = Instantiate(waveTextPrefab, UI.transform);
                g2.transform.localPosition = Vector3.zero;
                
                textSpawned = true;

            }

            if(index == maxEnemyCount)
            {
                canWait = true;
                textSpawned = false;

            }
        }
    }

    float Approach(float current, float target, float spd)
    {
        if (current < target)
        {
            current += spd;
            if (current > target)
                return target;
        }
        else
        {
            current -= spd;
            if (current < target)
                return target;
        }
        return current;
    }

}
