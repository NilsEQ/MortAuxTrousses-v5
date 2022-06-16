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

    public Camera mycamera;



    // Start is called before the first frame update
    void Awake()
    {
        EyeData = GameObject.Find("EyeData");
        transition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transition)
        {
            StartCoroutine(doTransition(currentRig, nextRig));
            currentRig = nextRig;
            mycamera = nextRig.GetComponentInChildren<Camera>();
            transition = false;
        }
    }



    public IEnumerator doTransition(GameObject previousRig, GameObject nextRig)
    {
        yield return 0;


        previousRig.SetActive(false);
        nextRig.SetActive(true);

        EyeData.GetComponent<Looking_tracker>().mycamera = mycamera;
        EyeData.GetComponent<Looking_tracker>().timer = 0.0f;


        yield return new WaitForEndOfFrame();
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        byte[] bytes = screenShot.EncodeToPNG();
        string fileName = Application.dataPath + "/" + previousRig.name + " to " + nextRig.name + " - After.png";
        System.IO.File.WriteAllBytes(fileName, bytes);
    }
}
