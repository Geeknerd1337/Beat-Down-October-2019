using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour
{

    private float alph = 1;
    private Text t;
    public Text t2;
    public Text t3;

     
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        t.text = "Wave " + WaveManager.WaveManagerInstance.wave.ToString();
        t2.text = "Wave " + WaveManager.WaveManagerInstance.wave.ToString();
        t3.text = "Wave " + WaveManager.WaveManagerInstance.wave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Color c = t.color;
        c.a = alph;
        t.color = c;
        alph -= Time.deltaTime/3f;
        Color c2 = t2.color;
        c2.a = alph;
        t2.color = c2;
        Color c3 = t3.color;
        c3.a = alph;
        t3.color = c3;

        if(alph <= 0)
        {
            Destroy(gameObject);
        }
        t.text = "Wave " + WaveManager.WaveManagerInstance.wave.ToString();
        t2.text = "Wave " + WaveManager.WaveManagerInstance.wave.ToString();
        t3.text = "Wave " + WaveManager.WaveManagerInstance.wave.ToString();
    }
}
