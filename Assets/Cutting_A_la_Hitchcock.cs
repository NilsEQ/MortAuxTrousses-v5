using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting_A_la_Hitchcock : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject myrig;
    Camera mycamera;

    public GameObject New_cameraPos;

    bool isfirsttime;

    void Awake()
    {
        myrig = GameObject.Find("Camera Rig");
        Debug.Log(myrig);
        mycamera = myrig.GetComponentInChildren<Camera>();
        isfirsttime = true;
    }

    private void OnEnable()
    {
        if (isfirsttime){
            isfirsttime = false;
        }
        else
        {
            float Eulerrotation_y = New_cameraPos.transform.rotation.eulerAngles.y - mycamera.transform.rotation.eulerAngles.y;

            myrig.transform.RotateAround(mycamera.transform.position, Vector3.up, Eulerrotation_y);

            Vector3 translation = New_cameraPos.transform.position - mycamera.transform.position;
            translation.y = New_cameraPos.transform.position.y - myrig.transform.position.y;
            myrig.transform.position += translation;
        }
    }

    private void Update()
    {
        Debug.Log(myrig.transform.rotation);
    }
}
