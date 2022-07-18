using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yRigOrientationController : MonoBehaviour
{

    public bool rotationAroundUser = true;

    public bool doTranslation = false;

    public SpeedTracker speedtracker;
    private Transform rigDepart;

    private Vector3 initialPos;
    private Quaternion initialRot;


    void Awake()
    {
        rigDepart = transform;
        speedtracker = GameObject.Find("Speedtracker").GetComponent<SpeedTracker>();
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    void OnEnable()
    {
        float angle = - speedtracker.headRotation.eulerAngles.y;
        
        if (rotationAroundUser){
            Vector3 position = transform.position + transform.rotation * speedtracker.headPosition * transform.localScale.x;
            Debug.Log("PrintOnEnable: script was enabled");
            transform.RotateAround(position, Vector3.up, angle);
        }
        else
        {
            transform.Rotate(Vector3.up, angle);
        }

        if (doTranslation)
        {
            Debug.Log("you're here");
            Vector3 cameraposition = transform.position + transform.rotation * speedtracker.headPosition * transform.localScale.x;
            Vector3 translation = initialPos - cameraposition;
            translation.y = 0;
            transform.position += translation;
        }

    }



    void OnDisable()
    {
        transform.rotation = initialRot;
        transform.position = initialPos;
    }

    /*
    private voids Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, yImmersantOrientation.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    */

}
