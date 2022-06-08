using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yRigOrientationController : MonoBehaviour
{
    public GameObject yImmersantOrientation;

    private Transform rigDepart;

    void Awake()
    {
        rigDepart = transform;
    }

    void OnEnable()
    {
        
        Debug.Log("PrintOnEnable: script was enabled");
        transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - yImmersantOrientation.transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void OnDisable()
    {
        transform.localRotation = rigDepart.localRotation;
    }
    /*
    private void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, yImmersantOrientation.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    */

}
