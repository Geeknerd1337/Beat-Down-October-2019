﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{


    public Text creditsText;
    private Player player;
    public FirstPersonController fps;
    public GameObject shopInterface;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ChangeState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeState();
        }
        UpdateCreditText();
    }

    void UpdateCreditText()
    {
        creditsText.text = " Crystals: " + player.money.ToString(); 
    }


    public void ChangeState()
    {
        shopInterface.SetActive(!shopInterface.activeSelf);

        if (shopInterface.activeSelf)
        {
            fps.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            FindObjectOfType<CameraMovement>().enabled = false;
            FindObjectOfType<PlayerController>().enabled = false;

        }
        else
        {
            fps.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FindObjectOfType<CameraMovement>().enabled = true;
            FindObjectOfType<PlayerController>().enabled = true;
        }
    }
}
