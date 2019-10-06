using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBaseSpinner : MonoBehaviour
{

    private float rot;
    SongManager songManager;
    // Start is called before the first frame update
    void Start()
    {
        rot = -15f;
        songManager = SongManager.ManagerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();   
    }

    void UpdateRotation()
    {
        if (Input.GetMouseButton(0))
        {

            if (songManager.beatFull)
            {
                rot += 60f;
            }

        }
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, rot), 0.15f);

        if(rot >= 360f + 15)
        {
            rot = -15f;
        }
    }
}
