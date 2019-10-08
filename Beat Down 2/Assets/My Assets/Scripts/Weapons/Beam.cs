using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public LineRenderer l;
    private float time = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
