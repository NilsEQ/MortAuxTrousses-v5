using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableVis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject transitions = GameObject.Find("Transitions");

        foreach (Transform transition in transitions.transform)
        {
            int count = transition.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform rig = transition.GetChild(i);
                rig.gameObject.SetActive(false);
            }
        }
    }
}
