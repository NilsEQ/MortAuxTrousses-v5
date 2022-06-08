using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;


public class Do_transition : MonoBehaviour
{

    public bool transition;
    public GameObject rig1;
    public GameObject rig2;

    // Start is called before the first frame update
    void Start()
    {
        transition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transition)
        {
            StartCoroutine(doTransition(rig1, rig2));
            transition = false;
        }
    }



    public IEnumerator doTransition(GameObject previousRig, GameObject nextRig)
    {
        yield return 0;


        previousRig.SetActive(false);
        nextRig.SetActive(true);

        GameObject EyeData = GameObject.Find("EyeData");
        EyeData.GetComponent<Looking_tracker>().mycamera = nextRig.GetComponentInChildren<Camera>();


        yield return new WaitForEndOfFrame();
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        byte[] bytes = screenShot.EncodeToPNG();
        string fileName = Application.dataPath + "/" + previousRig.name + " to " + nextRig.name + " - After.png";
        System.IO.File.WriteAllBytes(fileName, bytes);
    }
}
