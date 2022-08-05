using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_MainScript : MonoBehaviour
{

    private GameObject Transition_Handler;
    private Rig_Handler Rig_Handler;


    // Start is called before the first frame update

    //A parameter called in order to not do a transition when the scene begins (a weird quirk of Unity)
    private bool isfirsttime;

    //Our rigs
    public GameObject previousRig;
    public GameObject nextRig;

    private Transition_Conditions Conditions;

    public bool doTransitionWhenFail = true;

    // Start is called before the first frame update
    private void Awake()
    {
        isfirsttime = true;

        Transition_Handler = GameObject.Find("Transition_Handler");
        Rig_Handler = Transition_Handler.GetComponent<Rig_Handler>();

        Conditions = GetComponent<Transition_Conditions>();
    }

    void Start()
    {


    }

// Update is called once per frame
    void Update()
    {
        if (Conditions.verify() && !isfirsttime) doTransition();
    }

    private void OnDisable()
    {
        if (doTransitionWhenFail)
        {
            if (!isfirsttime)
            {
                doTransition();
            }
            else
            {
                isfirsttime = false;
            }
        }

    }

    private void doTransition()
    {
        if (Rig_Handler.currentRig == previousRig)
        {
            Rig_Handler.nextRig = nextRig;
            Rig_Handler.Transition_RigModifs = GetComponent<Transition_RigModifs>();
            Rig_Handler.Transition_Audio = GetComponent<Transition_Audio>();
            Rig_Handler.transition = true;
        }

    }


}
