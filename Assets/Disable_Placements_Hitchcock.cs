using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Placements_Hitchcock : MonoBehaviour
{

    Transform Rigs;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Hey");

        Rigs = GameObject.Find("Placements").transform;

        foreach (Transform Rig in Rigs)
        {
            foreach (Transform Child in Rig)
            {
                if (Child.gameObject.name == "OscarFS1")
                {
                    Child.gameObject.SetActive(false);
                }
            }
        }
    }


}
