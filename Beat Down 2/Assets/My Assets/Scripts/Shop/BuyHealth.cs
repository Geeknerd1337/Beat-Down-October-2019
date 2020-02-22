using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BuyHealth : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Player player;
    public Text namePlate;
    public string ammoName;
    public int cost;
    public Button myButton;
    public Text descriptionText;
    [TextArea(3, 10)]
    public string descrtiption;

    public int ammoType;
    public int ammoAmt;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        namePlate.text = ammoName + "[" + ammoAmt.ToString() + "]";
    }

    public void PurchaseAmmo()
    {
        if (player.money >= cost)
        {
            player.money -= cost;
            player.playerHealth += ammoAmt;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        descriptionText.text = ammoName + ".txt Loaded:\nCost[" + cost.ToString() + "] to heal for [" + ammoAmt.ToString() + "] points\n" + descrtiption;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = "";
    }
}
