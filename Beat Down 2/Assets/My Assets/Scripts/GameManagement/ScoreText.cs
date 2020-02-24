using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text t;
    private float a;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        a = Mathf.Lerp(a, GameManager.GameManagerInstance.score, Time.deltaTime * 2f);
        t.text = "Score: " + Mathf.Round(a).ToString();
    }
}
