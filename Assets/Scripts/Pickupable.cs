using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Assigned to anything that can be picked up, in this case only boxes. 
/// </summary>
public class Pickupable : MonoBehaviour
{
    Rigidbody rb;
    public int boxId;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Called when an object is picked up. 
    /// </summary>
    /// <param name="holder"></param>
    public void SetPickedUpState(Transform holder)
    {
        rb.useGravity = false; //Stops it from trying to fall when being held
        rb.isKinematic = true; //Stops the box from floating away and interfering with other objects
        transform.parent = holder; //Sets the parent to be the holder object that is childed to the camera. (This way the object moves when the player turns his head)
    }

    /// <summary>
    /// Called from the PickUp script when the player right clicks
    /// </summary>
    public void SetDroppedState()
    {
        rb.useGravity = true;
        transform.parent = null;
        rb.isKinematic = false;
    }

    /// <summary>
    /// Retuns the box Id for this box. Used to check to see if the correct box has been placed on the correct pad
    /// </summary>
    /// <returns></returns>
    public int ReturnBoxId()
    {
        return boxId;
    }
}
