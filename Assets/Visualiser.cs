using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Visualiser : MonoBehaviour
{

    GameObject rig1;
    GameObject rig2;



    public void UpdateVis()
    {
            GameObject transitions = GameObject.Find("Transitions");

            foreach (Transform transition in transitions.transform)
            {
                int count = transition.childCount;

                for (int i = 0; i < count; i++)
                {
                    Transform rig = transition.GetChild(0);
                    DestroyImmediate(rig.gameObject);
                }

            rig1 = Instantiate(transition.gameObject.GetComponent<Transition_Conditions>().previousRig);
            rig1.GetComponentInChildren<Camera>().targetDisplay = 2;
            rig1.transform.parent = transition;
            rig1.SetActive(true);

            rig2 = Instantiate(transition.gameObject.GetComponent<Transition_Conditions>().nextRig);
            rig2.GetComponentInChildren<Camera>().targetDisplay = 3;
            rig2.transform.parent = transition;
            rig2.SetActive(true);
        } 
    }

    public void RemoveVis()
    {
        GameObject transitions = GameObject.Find("Transitions");

        foreach (Transform transition in transitions.transform)
        {
            int count = transition.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform rig = transition.GetChild(0);
                DestroyImmediate(rig.gameObject);
            }
        }
    }


}
