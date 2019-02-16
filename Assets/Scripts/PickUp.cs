using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Very basic pick up script. As I mention in the accompanying video, there are definitely bugs with it. For the purpose of this tutorial it does the job, but I would reccommened imoproving this. 
/// </summary>
public class PickUp : MonoBehaviour
{

    public Transform holdPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 50.0f, Color.red);
        CheckForPickup();
    }

    /// <summary>
    /// Checks to see if the player is looking at a box
    /// </summary>
    private void CheckForPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20.0f)) //Casts a ray out in front of the player 
        {
            if (Input.GetKey(KeyCode.Mouse0)) //If the player presses the left mouse button...
            {
                if (hit.transform.GetComponent<Pickupable>()) //...and the raycast is hitting a box (only boxes have the Pickupable component)...
                {
                    hit.transform.GetComponent<Pickupable>().SetPickedUpState(transform); //...then set the pick up state of the box

                }
            }

            if (Input.GetKey(KeyCode.Mouse1)) 
            {
                if (hit.transform.GetComponent<Pickupable>()) 
                {
                    hit.transform.GetComponent<Pickupable>().SetDroppedState();

                }
            }

        }
    }
}
