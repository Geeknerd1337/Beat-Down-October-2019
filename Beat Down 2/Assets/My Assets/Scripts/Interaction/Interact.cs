using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Interact : MonoBehaviour
{

    public float range = 10f;
    public Camera fpsCam;

    public PlayerController playerController;
    public CameraMovement cameraMovement;
    public LayerMask lm;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && playerController.enabled)
        {

                InteractWith();
        }
        //Debug.DrawRay (fpsCam.transform.position, fpsCam.transform.forward);
    }

    void InteractWith()
    {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, lm))
        {
            Interaction target = hit.collider.transform.GetComponent<Interaction>();
            Debug.Log(hit.transform.name);
            if (target != null)
            {
                target.interaction();
                Debug.Log (target.name);
            }
        }


    }
}
