using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class Initiate : MonoBehaviour
{

    public GameObject firstRig;

    // Start is called before the first frame update
    void OnEnable()
    {
        firstRig.SetActive(true);

        GameObject EyeData = GameObject.Find("EyeData");
        EyeData.GetComponent<Looking_tracker>().mycamera = firstRig.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
