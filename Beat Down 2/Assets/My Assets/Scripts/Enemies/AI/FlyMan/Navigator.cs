using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    private float time = 0;
    public float timer = 3f;
    private NavMeshAgent agent;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Search();
    }

    void Search()
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            agent.SetDestination(player.position);
            time = 0;

        }



    }
}
