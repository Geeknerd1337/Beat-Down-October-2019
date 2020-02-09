using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIText : MonoBehaviour
{
    public Text creditsText;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        creditsText.text = "Crystals: " + player.money.ToString();
    }
}
