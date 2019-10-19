using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescreaseSize : MonoBehaviour
{
    private float oldScale = 1.63968f;
    private float newScale = 3f;
    private float time;
    private float initTime;
    public Color c;
    SongManager s;
    // Start is called before the first frame update
    void Start()
    {
        s = SongManager.ManagerInstance;
        GetComponent<Image>().color = c;
        initTime = s.GetSP();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(new Vector3(newScale, newScale, newScale), new Vector3(oldScale, oldScale, oldScale), time / s.GetSecPerBeat());
        time = s.GetSP()  - initTime;


        if(transform.localScale == new Vector3(oldScale, oldScale, oldScale))
        {
            Destroy(this.gameObject);
        }
    }
}
