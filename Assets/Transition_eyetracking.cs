//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;


namespace ViveSR.anipal.Eye
{
    public class Transition_eyetracking : MonoBehaviour
    {
        private FocusInfo FocusInfo;
        private readonly float MaxDistance = 20;
        private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };
        private static EyeData eyeData = new EyeData();
        private bool eye_callback_registered = false;


        private GameObject Transition_Handler;
        private bool done;

        public GameObject previousRig;
        public GameObject nextRig;

        private Camera mycamera;
        public GameObject toWatch;

        public float delay;
        private float timer;


        private void Start()
        {
            if (!SRanipal_Eye_Framework.Instance.EnableEye)
            {
                enabled = false;
                return;
            }
            mycamera = previousRig.GetComponentInChildren<Camera>();
            Transition_Handler = GameObject.Find("Transition_Handler");
        }



        void OnEnable()
        {
            done = false;
            timer = delay;
        }
        private void OnDisable()
        {
            doTransition();
        }

        private void Update()
        {
            if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

            if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
            {
                SRanipal_Eye.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                eye_callback_registered = true;
            }
            else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
            {
                SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                eye_callback_registered = false;
            }

            foreach (GazeIndex index in GazePriority)
            {
                Ray GazeRay;
                bool eye_focus;
                if (eye_callback_registered)
                    eye_focus = SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, 0, MaxDistance, eyeData, mycamera);
                else
                    eye_focus = SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, 0, MaxDistance, mycamera);

                if (eye_focus)
                {
                    Debug.Log(FocusInfo.transform.name);
                    GameObject seen = FocusInfo.transform.gameObject;
                    Debug.Log(seen);
                    if (seen == toWatch)
                    {
                        countdown();
                    }
                    else
                    {
                        timer = delay;
                    }
                    break;
                }
                else
                {
                    if (index == GazePriority[GazePriority.Length - 1])
                    {
                        timer = delay;
                    }
                }
            }
        }
        private void Release()
        {
            if (eye_callback_registered == true)
            {
                SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                eye_callback_registered = false;
            }
        }
        private static void EyeCallback(ref EyeData eye_data)
        {
            eyeData = eye_data;
        }

        private void doTransition()
        {
            if (!done)
            {
                ScreenCapture.CaptureScreenshot(Application.dataPath + "/" + previousRig.name + " to " + nextRig.name + " - Before.png");
                Transition_Handler.GetComponent<Do_transition>().rig1 = previousRig;
                Transition_Handler.GetComponent<Do_transition>().rig2 = nextRig;
                Transition_Handler.GetComponent<Do_transition>().transition = true;
                done = true;
            }

        }

        private void countdown()
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                doTransition();
            }
        }

        private IEnumerator TakeScreenshot(int ImageNumber)
        {
            yield return new WaitForEndOfFrame();
            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenShot.Apply();
            byte[] bytes = screenShot.EncodeToPNG();
            int index = ImageNumber + 1;
            string fileName = Application.dataPath + "/" + previousRig.name + " to " + nextRig.name + " - After.png";
            System.IO.File.WriteAllBytes(fileName, bytes);
        }
    }
}

