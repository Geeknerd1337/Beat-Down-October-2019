using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour
{

    private Vector3 origScale;
    public Vector3 newScale;
    // Start is called before the first frame update
    void Start()
    {
        origScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, origScale, 10f * Time.deltaTime);

        if (SongManager.ManagerInstance.beatFull)
        {
            transform.localScale = newScale;
        }
    }
}
