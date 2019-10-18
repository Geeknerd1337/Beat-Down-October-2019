using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuyGun : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Player player;
    public GameObject gun;
    public string gunName;
    public Text namePlate;
    public int cost;
    public Button myButton;
    public Text descriptionText;
    [TextArea(3,10)]
    public string descrtiption;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButton();
    }

    void UpdateButton()
    {
        namePlate.text = gunName; 
    }

    public void PurchaseWeapon()
    {
        if(player.money >= cost)
        {
            player.money -= cost;
            myButton.interactable = false;
            player.guns.Add(gun);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        descriptionText.text = gunName + ".txt Loaded:\nCost[" + cost.ToString() +"]\n" + descrtiption;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = "";
    }
}
