using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BasicEnemyAI : MonoBehaviour
{

    private float time = 0;
    public float timer = 3f;
    public float shootDistance;
    public float shootTime;
    private float shootTimer;


    private NavMeshAgent agent;
    [SerializeField]
    private Transform player;

    public GameObject projectilePrefab;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FindObjectOfType<NavMeshSurface>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        Search();
    }

    private void Awake()
    {
        FindObjectOfType<NavMeshSurface>().enabled = false;
    }

    void Search()
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            agent.SetDestination(player.position);
            time = 0;

        }

        if(Vector3.Distance(transform.position,player.position) < shootDistance)
        {
            shootTimer += Time.deltaTime;
            if(shootTimer > shootTime)
            {
                shootTimer = 0;
                Shoot();
            }
        }
        else
        {
            shootTimer = 0;
        }


        
    }

    void Shoot()
    {
        float angle = 10;
        if (Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle)
        {

            GameObject vfx;
            if (firePoint != null)
            {
                vfx = Instantiate(projectilePrefab);
                vfx.transform.SetParent(firePoint);
                vfx.transform.localPosition = Vector3.zero;
                vfx.transform.localRotation = Quaternion.Euler(Vector3.zero);

                vfx.GetComponent<ProjectileMove>().creator = firePoint;
                vfx.GetComponent<ProjectileMove>().hurtPlayer = true;
                vfx.transform.SetParent(null);
                vfx.layer = 13;
                Destroy(vfx, 5f);
            }
        }
    }

}
