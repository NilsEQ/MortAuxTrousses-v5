using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotinFrameTimer : MonoBehaviour
{
    private frustum_calc frustum_Calc;
    public GameObject toDetect;
    public float timer;
    Plane[] planes;

    //To detect change of rig
    private GameObject Transition_Handler;
    private Rig_Handler Rig_Handler;
    private GameObject rig;


    // Start is called before the first frame update
    void Start()
    {
        frustum_Calc = GetComponentInParent<frustum_calc>();

        Transition_Handler = GameObject.Find("Transition_Handler");
        Rig_Handler = Transition_Handler.GetComponent<Rig_Handler>();
        rig = Transition_Handler.GetComponent<Rig_Handler>().currentRig;

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rig != Rig_Handler.currentRig)
        {
            rig = Rig_Handler.currentRig;
            timer = 0.0f;
        }

        planes = frustum_Calc.planes;
        if (!GeometryUtility.TestPlanesAABB(planes, toDetect.GetComponent<Collider>().bounds))
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
        }
    }
}
