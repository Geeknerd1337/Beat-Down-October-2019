using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : Interaction
{

    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void interaction()
    {
        UI.SetActive(true);
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<CameraMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
