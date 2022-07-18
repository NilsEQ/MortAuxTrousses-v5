using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using System;

public class FadeToBlack_circle : MonoBehaviour
{

    private PostProcessVolume volume;
    private SpeedTracker speedtracker;

    public float fadeBeginning = 1f;
    public float fadeEnd = 1.5f;
    public float endValue = 0.99f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject VolumeObject = GameObject.Find("postProcessingVolume");
        volume = VolumeObject.GetComponent<PostProcessVolume>();
        speedtracker = GetComponentInParent<SpeedTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (float)Math.Sqrt(Math.Pow(speedtracker.headPosition.x, 2) + Math.Pow(speedtracker.headPosition.z, 2));

        if (dist < fadeBeginning)
        {
            volume.weight = 0;
        }
        else
        {

            if (dist > fadeEnd)
            {
                volume.weight = endValue;
            }
            else
            {

                volume.weight = endValue * (dist - fadeBeginning) / (fadeEnd - fadeBeginning);
            }
        }
    }
}
