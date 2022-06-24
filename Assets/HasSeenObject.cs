using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class HasSeenObject : MonoBehaviour
{
    private Looking_tracker looking_Tracker;
    private Rig_Handler Rig_Handler;
    private GameObject rig;

    public GameObject to_see;
    public bool isokay;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {

        looking_Tracker = GetComponentInParent<Looking_tracker>();

        GameObject Transition_Handler = GameObject.Find("Transition_Handler");
        Rig_Handler = Transition_Handler.GetComponent<Rig_Handler>();
        rig = Transition_Handler.GetComponent<Rig_Handler>().currentRig;

        isokay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rig != Rig_Handler.currentRig)
        {
            rig = Rig_Handler.currentRig;
            isokay = false;
            timer = 0.0f;
        }

        if (looking_Tracker.seen == to_see)
        {
            isokay = true;
        }

        if (isokay)
        {
            timer += Time.deltaTime;
        }
    }
}
