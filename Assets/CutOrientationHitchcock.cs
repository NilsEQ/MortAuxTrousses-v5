using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class CutOrientationHitchcock : MonoBehaviour
{

    public bool rotationAroundUser = true;

    public bool doTranslation = true;

    public SpeedTracker speedtracker;
    private Transform rigDepart;

    private Vector3 initialPos;
    private Quaternion initialRot;

    Looking_tracker looking_Tracker;
    Data_writer_Hitchcock data_Writer;


    void Awake()
    {
        rigDepart = transform;
        speedtracker = GameObject.Find("Speedtracker").GetComponent<SpeedTracker>();
        initialPos = transform.position;
        initialRot = transform.rotation;

        looking_Tracker = GameObject.Find("EyeData").GetComponent<Looking_tracker>();

        data_Writer = GameObject.Find("Data_writer").GetComponent<Data_writer_Hitchcock>();

    }

    void OnEnable()
    {


        looking_Tracker.mycamera = GetComponentInChildren<Camera>();

        data_Writer.isCut = true;

        data_Writer.RigName = name;

        float angle = -speedtracker.headRotation.eulerAngles.y;

        if (rotationAroundUser)
        {
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
    private void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, yImmersantOrientation.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    */

}

