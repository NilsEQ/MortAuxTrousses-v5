using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;


public class Rig_Handler : MonoBehaviour
{

    public bool transition;
    public GameObject currentRig;
    public GameObject nextRig;
    GameObject EyeData;
    GameObject FrameHandler;

    public Camera mycamera;

    public string path;

    //variable for method of transition
    public Transition_RigModifs Transition_RigModifs;
    public Transition_Audio Transition_Audio;

    // Start is called before the first frame update
    void Awake()
    {
        EyeData = GameObject.Find("EyeData");
        FrameHandler = GameObject.Find("Frame_Handler");
        transition = false;
    }

    private void Start()
    {
        if (path is null)
        {
            path = Application.dataPath + "/Data/pictures";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transition)
        {
            //ScreenCapture.CaptureScreenshot(path + "/" + currentRig.name + " to " + nextRig.name + " - Before.png");
            StartCoroutine(doTransition(currentRig, nextRig));

            currentRig = nextRig;
            mycamera = nextRig.GetComponentInChildren<Camera>(true);
            transition = false;
        }
    }



    public IEnumerator doTransition(GameObject previousRig, GameObject nextRig)
    {

        
        previousRig.SetActive(false);
        GameObject CameraOfpreviousRig = previousRig.transform.Find("Camera").gameObject;
        GameObject AudioOfpreviousRig = previousRig.transform.Find("Audio").gameObject;

        GameObject CameraOfnextRig = nextRig.transform.Find("Camera").gameObject;
        GameObject AudioOfnextRig = nextRig.transform.Find("Audio").gameObject;



        CameraOfnextRig.SetActive(false);
        AudioOfnextRig.SetActive(false);
        nextRig.SetActive(true);


        //modify the next rigs placement
        Rig_Modifs Rig_modifier = nextRig.GetComponent<Rig_Modifs>();
        if (Transition_RigModifs.RotateAroundCenter)
        {
            Debug.Log("rotate");
            Rig_modifier.rotationAroundCenter(Transition_RigModifs.angle_correction);
        }
        else { 
        if (Transition_RigModifs.RotateAroundUser)
        {
                Rig_modifier.rotationAroundUser(Transition_RigModifs.angle_correction);
        }
        }

        if (Transition_RigModifs.translate)
        {
            Debug.Log("translate");
            Rig_modifier.translate();
        }

        Debug.Log("Here is okay");

        //wait one frame before changing camera to avoid jumps in the image
        yield return 0;
        CameraOfnextRig.SetActive(true);
        AudioOfnextRig.SetActive(true);
        previousRig.SetActive(false);

        //if (Transition_Audio.SameTime)
        //{
        //    CameraOfnextRig.SetActive(true);
        //    AudioOfnextRig.SetActive(true);
        //    previousRig.SetActive(false);
        //}
        //else
        //{
        //    if (Transition_Audio.AudioLast)
        //    {
        //        CameraOfnextRig.SetActive(true);
        //        CameraOfpreviousRig.SetActive(false);

        //        yield return new WaitForSeconds(Transition_Audio.delay);

        //        AudioOfnextRig.SetActive(true);
        //        previousRig.SetActive(false);

        //    }
        //    else
        //    {
        //        AudioOfnextRig.SetActive(true);
        //        AudioOfpreviousRig.SetActive(false);

        //        yield return new WaitForSeconds(Transition_Audio.delay);

        //        CameraOfnextRig.SetActive(true);
        //        previousRig.SetActive(false);
        //    }
        //}



        EyeData.GetComponent<Looking_tracker>().mycamera = mycamera;
        EyeData.GetComponent<Looking_tracker>().timer = 0.0f;
        FrameHandler.GetComponent<frustum_calc>().mycamera = mycamera;


        //yield return new WaitForEndOfFrame();
        //Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //screenShot.Apply();
        //byte[] bytes = screenShot.EncodeToPNG();
        //string fileName = path + "/" + previousRig.name + " to " + nextRig.name + " - After.png";
        //System.IO.File.WriteAllBytes(fileName, bytes);
    }


}
