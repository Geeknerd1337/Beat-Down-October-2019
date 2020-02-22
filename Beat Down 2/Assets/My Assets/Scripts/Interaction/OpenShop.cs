using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : Interaction
{

    public GameObject UI;
    public ShopScript s;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<ShopScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void interaction()
    {
        s.ChangeState();
    }
}
