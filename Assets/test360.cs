using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test360 : MonoBehaviour
{

    //public GameObject test;
    // Start is called before the first frame update
    public RenderTexture cubemap1;
    public RenderTexture cubemap2;
    public RenderTexture equirect;
    public bool renderStereo = true;
    public float stereoSeparation = 0.064f;

    void LateUpdate()
    {
        Camera cam = GetComponent<Camera>();

        if (cam == null)
        {
            cam = GetComponentInParent<Camera>();
        }

        if (cam == null)
        {
            Debug.Log("stereo 360 capture node has no camera or parent camera");
        }

        if (renderStereo)
        {
            cam.stereoSeparation = stereoSeparation;
            cam.RenderToCubemap(cubemap1, 63, Camera.MonoOrStereoscopicEye.Left);
            cam.RenderToCubemap(cubemap2, 63, Camera.MonoOrStereoscopicEye.Right);
        }
        else
        {
            cam.RenderToCubemap(cubemap1, 63, Camera.MonoOrStereoscopicEye.Mono);
        }

        //optional: convert cubemaps to equirect

        if (equirect == null)
            return;

        cubemap1.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Left);
        cubemap2.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Right);
    }
}
