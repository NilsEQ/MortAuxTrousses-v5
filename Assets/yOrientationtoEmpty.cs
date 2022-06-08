using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yOrientationtoEmpty : MonoBehaviour
{

    public GameObject yOrientationCameraRecorder;
        
    // Update is called once per frame
    void OnDisable()
    {
        yOrientationCameraRecorder.transform.rotation = Quaternion.Euler(yOrientationCameraRecorder.transform.eulerAngles.x, transform.localEulerAngles.y, yOrientationCameraRecorder.transform.eulerAngles.z);
    }
}
