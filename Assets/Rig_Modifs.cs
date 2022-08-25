using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rig_Modifs : MonoBehaviour
{


    private SpeedTracker speedtracker;
    private Transform rigDepart;

    private Vector3 initialPos;
    private Quaternion initialRot;
    private Vector3 initialScale;

    void Awake()
    {
        rigDepart = transform;
        speedtracker = GameObject.Find("Speedtracker").GetComponent<SpeedTracker>();
        initialPos = transform.localPosition;
        initialRot = transform.localRotation;
        initialScale = transform.localScale;
    }


    public void rotationAroundCenter(float angle_corr)
    {
        float angle = -speedtracker.headRotation.eulerAngles.y;
        transform.Rotate(Vector3.up, angle + angle_corr);
    }


    public void rotationAroundUser(float angle_corr)
    {
        float angle = -speedtracker.headRotation.eulerAngles.y;
        Vector3 position = transform.position + transform.rotation * speedtracker.headPosition * transform.localScale.x;
        transform.RotateAround(position, Vector3.up, angle + angle_corr);
    }

    public void translate()
    {
        Vector3 cameraposition = transform.position + transform.rotation * speedtracker.headPosition * transform.localScale.x;
        Vector3 translation = initialPos - cameraposition;
        translation.y = 0;
        transform.position += translation;
    }


    void OnDisable()
    {
        transform.localRotation = initialRot;
        transform.localPosition = initialPos;
        transform.localScale = initialScale;
    }

    /*
    private voids Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, yImmersantOrientation.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    */

}
