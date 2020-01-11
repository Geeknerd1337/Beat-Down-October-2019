using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAnims : MonoBehaviour
{
    [SerializeField] Transform leftWing;
    [SerializeField] Transform rightWing; 
    [SerializeField] Transform armature;
    [SerializeField] Transform abdomen;
    [SerializeField] Transform antennaL;
    [SerializeField] Transform antennaR;
    private Quaternion initialLWrot;
    private Quaternion initialRWrot;
    private Quaternion initialAbRot;
    private Quaternion initialALRot;
    private Quaternion initialARRot;
    private Quaternion wingLOffset = Quaternion.Euler(0,0,45);
    private Quaternion wingROffset = Quaternion.Euler(0,0,-45);
    private int wingSpeed = 12;
    void Start(){
        initialLWrot = leftWing.localRotation;
        initialRWrot = rightWing.localRotation;

        initialAbRot = abdomen.localRotation;

        initialALRot = antennaL.localRotation;
        initialARRot = antennaR.localRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //rotate wings
        float wingRotC = Mathf.Cos(Time.fixedTime * wingSpeed);
        float wingRotS = Mathf.Sin(Time.fixedTime * wingSpeed);
        Quaternion rotQuatL = Quaternion.Euler(0,0,wingRotC*30);
        Quaternion rotQuatR = Quaternion.Euler(0,0,-wingRotC*30);
        leftWing.localRotation =  initialLWrot * wingLOffset * rotQuatL;
        rightWing.localRotation = initialRWrot * wingROffset * rotQuatR;

        //rotate antennas
        Quaternion rotQuatAL = Quaternion.Euler(wingRotC*10,0,0);
        Quaternion rotQuatAR = Quaternion.Euler(wingRotC*10,0,0);
        antennaL.localRotation = initialALRot * rotQuatAL;
        antennaR.localRotation = initialARRot * rotQuatAR;

        //move abdomen
        Quaternion rotQuatAb = Quaternion.Euler(wingRotS*5f,0,0);
        abdomen.localRotation = rotQuatAb * initialAbRot;
        
        //move body up and down
        armature.localPosition = new Vector3(0,wingRotS*0.1f,0);
    }
}
