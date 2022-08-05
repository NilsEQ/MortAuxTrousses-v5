//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;



namespace ViveSR.anipal.Eye
{
    public class Looking_tracker : MonoBehaviour
    {
        private FocusInfo FocusInfo;
        private readonly float MaxDistance = 20;
        private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };
        private readonly EyeIndex[] EyePriority = new EyeIndex[] {EyeIndex.LEFT};
        private static EyeData eyeData = new EyeData();
        private bool eye_callback_registered = false;


        public Camera mycamera;
        public GameObject eyetracked_object;
        private GameObject isnull;
        public float timer;

        public GameObject seen;

        public Ray GazeRay;



        private void Start()
        {
            if (!SRanipal_Eye_Framework.Instance.EnableEye)
            {
                enabled = false;
                return;
            }

            isnull = new GameObject("Nullreference");

        }




        void OnEnable()
        {
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
                bool eye_focus;
                if (eye_callback_registered)
                    eye_focus = SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, 0, MaxDistance, eyeData, mycamera);
                else
                    eye_focus = SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, 0, MaxDistance, mycamera);


                if (eye_focus)
                {
                    Debug.Log(FocusInfo.transform.name);
                    seen = FocusInfo.transform.gameObject;
                    Debug.Log(seen);
                    if (seen == eyetracked_object)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        timer = Time.deltaTime;
                        eyetracked_object = seen;
                    }
                    break;
                }
                else
                {
                    if (index == GazePriority[GazePriority.Length - 1])
                    {
                        eyetracked_object = isnull;
                        timer = 0.0f;
                    }
                } 
            }

            //foreach (EyeIndex eye in EyePriority)
            //{
            //    float openness;
            //    bool ye;
            //    if (eye_callback_registered)
            //        ye = SRanipal_Eye.GetEyeOpenness(eye, out openness, eyeData);
            //    else
            //        ye = SRanipal_Eye.GetEyeOpenness(eye, out openness);
            //    Debug.Log(ye);
            //}
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
    }
}
