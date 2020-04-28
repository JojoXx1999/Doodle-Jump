using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //The object that the camera should follow (the player)
    public Transform target;

    /*
    To be updated every frame, will run after every other update function has finished executing
    //*/
    void LateUpdate()
    {
        if (target.position.y > transform.position.y) //if the camera target is above the Y position of the camera
        {
            //Set the position of the camera to the target
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }

    public void Reset()
    {
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
    }
}
