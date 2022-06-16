using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class Initiate : MonoBehaviour
{

    public GameObject firstRig;

    private GameObject Transition_Handler;
    private Rig_Handler Rig_Handler;

    // Start is called before the first frame update

    private void Awake()
    {
        Transition_Handler = GameObject.Find("Transition_Handler");
        Rig_Handler = Transition_Handler.GetComponent<Rig_Handler>();
        Rig_Handler.currentRig = firstRig;
        Rig_Handler.mycamera = firstRig.GetComponentInChildren<Camera>();

        GameObject EyeData = GameObject.Find("EyeData");
        EyeData.GetComponent<Looking_tracker>().mycamera = firstRig.GetComponentInChildren<Camera>();
    }

    void Start()
    {
        firstRig.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
