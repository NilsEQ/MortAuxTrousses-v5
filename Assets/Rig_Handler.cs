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
            mycamera = nextRig.GetComponentInChildren<Camera>();
            transition = false;
        }
    }



    public IEnumerator doTransition(GameObject previousRig, GameObject CameraObject)
    {

        
        previousRig.SetActive(false);

        GameObject CameraOfRig = nextRig.transform.Find("Camera").gameObject;

        CameraObject.SetActive(false);
        nextRig.SetActive(true);

        yield return 0;
        previousRig.SetActive(false);
        CameraOfRig.SetActive(true);

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

    public void doTransitionNew(GameObject previousRig, GameObject nextRig)
    {
        previousRig.SetActive(false);
        
        EyeData.GetComponent<Looking_tracker>().mycamera = mycamera;
        EyeData.GetComponent<Looking_tracker>().timer = 0.0f;

        FrameHandler.GetComponent<frustum_calc>().mycamera = mycamera;

    }
}
