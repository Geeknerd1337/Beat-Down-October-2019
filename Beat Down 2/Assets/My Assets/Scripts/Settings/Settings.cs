using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public List<GameObject> objects;
    public FirstPersonController fps;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in objects)
        {
            g.SetActive(false);
        }
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!FindObjectOfType<ShopScript>().shopInterface.activeSelf)
            {
                ToggleUI();
            }
        }   
    }

    void ToggleUI()
    {
        open = !open;
        if (open)
        {
            foreach (GameObject g in objects)
            {
                g.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            FindObjectOfType<CameraMovement>().enabled = false;
            FindObjectOfType<PlayerController>().enabled = false;

        }
        else
        {
            foreach (GameObject g in objects)
            {
                g.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FindObjectOfType<CameraMovement>().enabled = true;
            FindObjectOfType<PlayerController>().enabled = true;
        }
    }
}
