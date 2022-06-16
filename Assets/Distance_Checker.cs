using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_Checker : MonoBehaviour
{

    private GameObject Transition_Handler;
    private Rig_Handler Rig_Handler;


    public float current_dist;
    public float threshold_dist;
    public GameObject toDistance;

    public Camera mycamera;

    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        Transition_Handler = GameObject.Find("Transition_Handler");
        Rig_Handler = Transition_Handler.GetComponent<Rig_Handler>();
        mycamera = Rig_Handler.mycamera;
    }

    // Update is called once per frame
    void Update()
    {
        if(mycamera != Rig_Handler.mycamera)
        {
            mycamera = Rig_Handler.mycamera;
            timer = 0.0f;
        }

        Collider mycollider = toDistance.GetComponent<Collider>();
        Vector3 mypoint = mycollider.ClosestPointOnBounds(mycamera.transform.position);
        current_dist = Vector3.Distance(mycamera.transform.position, mypoint);

        if (current_dist < threshold_dist)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0.0f;

        }
    }
}
