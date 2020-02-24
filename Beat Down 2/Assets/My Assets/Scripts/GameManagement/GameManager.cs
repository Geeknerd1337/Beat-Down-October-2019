using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager GameManagerInstance;
    public void Awake()
    {

        if (GameManagerInstance != null && GameManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public float score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
