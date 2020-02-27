using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowGrenade : MonoBehaviour
{
    private Vector3 oldScale;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        oldScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SongManager.ManagerInstance.CheckIfValidTimeWithinFirepattern())
            {
                transform.localScale = Vector3.zero;
            }
        }
        transform.localScale = Vector3.Lerp(transform.localScale, oldScale, speed * Time.deltaTime);
    }
}
