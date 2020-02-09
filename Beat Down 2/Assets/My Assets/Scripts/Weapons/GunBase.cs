using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunBase : MonoBehaviour
{

    //Vectors for the gun sitting in it's normal position
    [Header("Idle")]
    public Vector3 idlePosition;
    public Vector3 idleAnim;
    public Vector3 idleEuler;

    [Header("Gun Sway")]
    public float swayIntensity;
    public float smooth;
    [SerializeField]
    private Quaternion originRotation;

    //Vectors for when the gun is shot
    [Header("Shot")]
    public Vector3 shotPosition;
    public Vector3 shotEuler;

    [Header("Stuff having to do with music and beats")]
    public bool held;
    SongManager songManager;
    //The track that this gun is using
    public AudioSource myAudio;
    public float timeToIncreaseVolume;
    private float timer;
    private int volumeMod = 1;

    [Header("Guns that use Visualization")]
    public bool visualShot;
    private AudioPeer peer;
    public Vector2 audioCutOff;
    private bool canShoot = true;
    public float timeBetweenShots;
    private float shotTimer;


    [Header("Gun Visual Efffects")]
    public Transform laserPoint;
    public GameObject LaserPrefab;
    public LayerMask layermask;

    [Header("Gun Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float spread = 0f;
    public int numShots = 1;
    public int ammoType;
    public bool infiniteAmmo;


    [Header("Projetile Information")]
    public bool projectile;
    public List<GameObject> projectileVfx = new List<GameObject>();

    private GameObject effectTospawn;


    [Header("Sequencer")]
    public bool useSequencer;
    public bool useSequencer2;
    public bool[] sequence = new bool[64];
    public bool[] sequence2 = new bool[16];
    public bool[] firePattern = new bool[8];
    public bool[] chargePattern = new bool[8];


    [Header("Misc")]
    [SerializeField]
    private Player player;
    public TextMeshProUGUI ammoText;


    [Header("Guns that require to be charged for a bar")]
    public AudioSource chargeSound;
    public bool chargeShot;
    [SerializeField]
    private bool charging;
    private bool canReset;
    private bool chargeFire;

    public bool makeNoise;
    public GameObject noise;






    // Start is called before the first frame update
    void Start()
    {
        //Get the instance of the song manager
        songManager = SongManager.ManagerInstance;
        //Get the audio source on this weapon
        if (GetComponent<AudioSource>() != null)
        {
            myAudio = GetComponent<AudioSource>();
        }
        //Add it to song managers audioList
        //songManager.audioList.Add(myAudio);
        if (chargeSound != null)
        {
            songManager.audioList.Add(chargeSound);
            chargeSound.volume = 0;
        }
        //Set my volume down to zero
        myAudio.volume = 0;
        //Get the peer from my audioSource;
        peer = myAudio.gameObject.GetComponent<AudioPeer>();

        effectTospawn = projectileVfx[0];

        player = FindObjectOfType<Player>();
        originRotation = transform.localRotation;
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


        if(ammoText != null)
        {
            UpdateText();
        }
    }



    void LerpIdle()
    {

        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        Quaternion adjustment = Quaternion.AngleAxis(swayIntensity * -1f * mouse_x, Vector3.up);
        Quaternion adjustmentY = Quaternion.AngleAxis(swayIntensity * -1f * mouse_y, Vector3.right);
        Quaternion targetRotation = originRotation * adjustment * adjustmentY * Quaternion.Euler(idleEuler.x, idleEuler.y, idleEuler.z);

        transform.localPosition = Vector3.Lerp(transform.localPosition, idlePosition + new Vector3(Mathf.PerlinNoise(Time.time, Time.time) * idleAnim.x, Mathf.Sin(Time.time) * idleAnim.y, Mathf.PerlinNoise(Time.time, Time.time) * idleAnim.z), 0.3f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, 0.3f);
    }

    void Shoot()
    {

            if (player.ammoTypes[ammoType] > 0)
            {
            for (int i = 0; i < numShots; i++)

            {
                if (!infiniteAmmo)
                {
                    player.ammoTypes[ammoType]--;
                }
                RaycastHit hit;
                Vector3 dir = Camera.main.transform.forward;
                dir.x += Random.Range(-spread, spread);
                dir.y += Random.Range(-spread, spread);
                dir.z += Random.Range(-spread, spread);


                GameObject g;
                g = null;

                if (projectile)
                {
                    SpawnVFX(dir);

                }
                else
                {
                    g = Instantiate(LaserPrefab);
                    g.transform.position = laserPoint.position;
                    g.transform.rotation = laserPoint.rotation;
                    g.transform.SetParent(Camera.main.transform);
                }



                if (Physics.Raycast(Camera.main.transform.position, dir, out hit, range, layermask) && !projectile)
                {
                    Target target = hit.transform.GetComponent<Target>();



                    if (target != null)
                    {
                        g.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Vector3.Distance(Camera.main.transform.position, hit.point)));
                        target.TakeDamage(damage);

                    }

                }

            }
            transform.localRotation = Quaternion.Euler(shotEuler.x, shotEuler.y, shotEuler.z);
            transform.localPosition = idlePosition + shotPosition;
        }

        
        }


    void SpawnVFX(Vector3 dir)
    {
        GameObject vfx;
        if(laserPoint != null)
        {
            vfx = Instantiate(effectTospawn);
            vfx.transform.SetParent(laserPoint);
            vfx.transform.localPosition = Vector3.zero;
            vfx.transform.localRotation = Quaternion.Euler(dir);
            if (vfx.GetComponent<ProjectileMove>() != null)
            {
                vfx.GetComponent<ProjectileMove>().creator = laserPoint;
                vfx.GetComponent<ProjectileMove>().damage = damage;
                vfx.transform.SetParent(null);
            }


            if (makeNoise)
            {
                Instantiate(noise);
            }


        }
    }

    void HeldShot()
    {

        //This is the event that will check the song manager and do logic for if a shot can be held down
        volumeMod = 1;
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (!useSequencer)
            {
                if (songManager.beatFull)
                {
                    Shoot();
                }
            }
            else
            {
                
                if(songManager.beatFullD8 && !useSequencer2)
                {
                    if (songManager.beatFullD8 && sequence[(int)songManager.beatCount2] == true)
                    {
                        Shoot();
                    }
                }

                if(useSequencer2 && songManager.beatFullD16)
                {
                    if (sequence2[(int)songManager.beatCount4] == true)
                    {
                        Shoot();
                    }
                }


            }

        }else
        {
            timer -= Time.deltaTime;
        }


        
    }

    void TappedShot()
    {
        //This event is going to be used for if a shot has to be tapped to the beat, the track will always play in these instances, but only if this isn't a charged weapon
        if (!chargeShot)
        {
            timer += Time.deltaTime;
            if(player.combo == 0)
            {
                volumeMod = 0;
                Debug.Log("yellow");
            }

            if (makeNoise)
            {
                volumeMod = 0;

            }

            if (Input.GetMouseButtonDown(0))
            {

                if (songManager.CheckIfValidTimeWithinFirepattern())
                {
                    Shoot();
                    volumeMod = 1;
                }
                else
                {
                    volumeMod = 0;
                    
                }
            }

        }
        else
        {
            if (!charging)
            {
                timer = 0;
                if (Input.GetMouseButtonDown(0))
                {
                    if (songManager.CheckIfValidTimeWithinFirepattern())
                    {
                        charging = true;
                        timer = 0;
                        chargeSound.volume = 1;
                        canReset = false;
                        chargeFire = false;
                        volumeMod = 1;
                    }
                }
            }
            else
            {
                if (!chargeFire)
                {
                    if (songManager.beatCount3 == 1 && canReset)
                    {
                        charging = false;
                        chargeSound.volume = 0;
                    }

                    if (songManager.beatCount3 == 2)
                    {
                        canReset = true;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (songManager.CheckIfValidTimeWithinChargepattern() && canReset)
                        {
                            chargeFire = true;
                            timer = 1;
                            chargeSound.volume = 0;
                            Shoot();
                            canReset = false;
                        }
                    }
                }
                else
                {
                    if (songManager.beatCount3 == 2)
                    {
                        canReset = true;
                    }

                    if (songManager.beatCount3 == 0 && canReset)
                    {
                        charging = false;
                        chargeSound.volume = 0;
                    }
                }
            }
        }
    }

    void AdjustMyVolume()
    {
        myAudio.volume = Mathf.Lerp(0, 1, timer / timeToIncreaseVolume) * volumeMod;
        timer = Mathf.Clamp(timer, 0, timeToIncreaseVolume);
    }

    void AudioShot()
    {
        //This is the event that will check the song manager and do logic for if a shot can be held down and fires as a result of audio visualization
        volumeMod = 1;
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (peer.CheckAllBands(audioCutOff.x) && canShoot)
            {
                Shoot();
                canShoot = false;
                shotTimer = 0;
            }

            if (!canShoot)
            {
                shotTimer += Time.deltaTime;
            }

            if (!canShoot && shotTimer > timeBetweenShots & !peer.CheckAllBands(audioCutOff.x))
            {
                canShoot = true;
                
            }



        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void UpdateText()
    {
        if (!infiniteAmmo)
        {
            ammoText.text = player.ammoTypes[ammoType].ToString();
        }
        else
        {
            ammoText.text = "";
        }
    }
}
