using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay_Rig_Correction : MonoBehaviour
{


    public bool rotationAroundUser = false;

    public bool doTranslation = false;

    private Transform rigDepart;
    
    private Vector3 initialPos;
    private Quaternion initialRot;

    private lastCutData lastCutData;

    void Awake()
    {
        rigDepart = transform;
        initialPos = transform.position;
        initialRot = transform.rotation;
        lastCutData = GameObject.Find("lastCutData").GetComponent<lastCutData>();
    }

    void OnEnable()
    {

        float angle = -lastCutData.rotation.eulerAngles.y;

        if (rotationAroundUser)
        {
            Vector3 position = transform.position + transform.rotation * lastCutData.position * transform.localScale.x;
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
            Vector3 cameraposition = transform.position + transform.rotation * lastCutData.position * transform.localScale.x;
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
