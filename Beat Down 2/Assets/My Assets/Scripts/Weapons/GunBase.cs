using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    //Vectors for the gun sitting in it's normal position
    [Header("Idle")]
    public Vector3 idlePosition;
    public Vector3 idleAnim;
    public Vector3 idleEuler;

    //Vectors for when the gun is shot
    [Header("Shot")]
    public Vector3 shotPosition;
    public Vector3 shotEuler;

    [Header("Stuff having to do with music and beats")]
    public bool held;
    SongManager songManager;
    //The track that this gun is using
    private AudioSource myAudio;
    public float timeToIncreaseVolume;
    private float timer;

    [Header("Guns that use Visualization")]
    public bool visualShot;
    private AudioPeer peer;
    public float audioCutOff;
    private bool canShoot = true;


    [Header("Gun Visual Efffects")]
    public Transform laserPoint;
    public GameObject LaserPrefab;
    public LayerMask layermask;

    [Header("Gun Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float spread = 0f;

 



    // Start is called before the first frame update
    void Start()
    {
        //Get the instance of the song manager
        songManager = SongManager.ManagerInstance;
        //Get the audio source on this weapon
        myAudio = GetComponent<AudioSource>();
        //Add it to song managers audioList
        songManager.audioList.Add(myAudio);
        //Set my volume down to zero
        myAudio.volume = 0;
        //Get the peer from my audioSource;
        peer = GetComponent<AudioPeer>();
    }

    // Update is called once per frame
    void Update()
    {
        LerpIdle();
        AdjustMyVolume();

        if (held)
        {
            HeldShot();
        }
        else if (visualShot)
        {
            AudioShot();
        }else
        {
            TappedShot();
        }
    }


    void LerpIdle()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, idlePosition + new Vector3(Mathf.PerlinNoise(Time.time, Time.time) * idleAnim.x, Mathf.Sin(Time.time) * idleAnim.y, Mathf.PerlinNoise(Time.time, Time.time) * idleAnim.z), 0.3f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(idleEuler.x, idleEuler.y, idleEuler.z), 0.3f);
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 dir = Camera.main.transform.forward;
        dir.x += Random.Range(-spread, spread);
        dir.y += Random.Range(-spread, spread);
        dir.z += Random.Range(-spread, spread);


        GameObject g = Instantiate(LaserPrefab);
        g.transform.position = laserPoint.position;
        g.transform.rotation = laserPoint.rotation;
        g.transform.SetParent(Camera.main.transform);

        transform.localRotation = Quaternion.Euler(shotEuler.x, shotEuler.y, shotEuler.z);
        transform.localPosition = idlePosition + shotPosition;

        if (Physics.Raycast(Camera.main.transform.position, dir, out hit, range, layermask))
        {
            Target target = hit.transform.GetComponent<Target>();
            g.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0,0, Vector3.Distance(Camera.main.transform.position, hit.point)));

            if (target != null)
            {
                target.TakeDamage(damage);

            }

        }
        
        }

    void HeldShot()
    {

        //This is the event that will check the song manager and do logic for if a shot can be held down
        
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (songManager.beatFull)
            {
                Shoot();
            }

        }else
        {
            timer -= Time.deltaTime;
        }


        
    }

    void TappedShot()
    {
        //This event is going to be used for if a shot has to be tapped to the beat, the track will always play in these instances
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (songManager.CheckIfValidTime())
            {
                Shoot();
            }
        }
    }

    void AdjustMyVolume()
    {
        myAudio.volume = Mathf.Lerp(0, 1, timer / timeToIncreaseVolume);
        timer = Mathf.Clamp(timer, 0, timeToIncreaseVolume);
    }

    void AudioShot()
    {
        //This is the event that will check the song manager and do logic for if a shot can be held down and fires as a result of audio visualization

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (peer.CheckAllBands(audioCutOff) && canShoot)
            {
                Shoot();
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }

        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
