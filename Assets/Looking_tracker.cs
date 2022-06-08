//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using IronXL;



namespace ViveSR.anipal.Eye
{
    public class Looking_tracker : MonoBehaviour
    {
        private FocusInfo FocusInfo;
        private readonly float MaxDistance = 20;
        private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };
        private static EyeData eyeData = new EyeData();
        private bool eye_callback_registered = false;


        public Camera mycamera;
        public GameObject LookedAtObject;
        private GameObject isnull;
        public float timer;
        private float global_timer;


        private void Start()
        {
            if (!SRanipal_Eye_Framework.Instance.EnableEye)
            {
                enabled = false;
                return;
            }

            isnull = new GameObject("Nullreference");


            //Create new Excel WorkBook document. 
            //The default file format is XLSX, but we can override that for legacy support
            WorkBook xlsWorkbook = WorkBook.Create(ExcelFileFormat.XLS);
            xlsWorkbook.Metadata.Author = "IronXL";

            //Add a blank WorkSheet
            WorkSheet xlsSheet = xlsWorkbook.CreateWorkSheet("new_sheet");
            //Add data and styles to the new worksheet

            xlsSheet["A1"].Value = "Hello World";
            xlsSheet["A2"].Style.BottomBorder.SetColor("#ff6600");
            xlsSheet["A2"].Style.BottomBorder.Type = IronXL.Styles.BorderType.Double;

            //Save the excel file
            xlsWorkbook.SaveAs(Application.dataPath + "/NewExcelFile.xls");
        }




        void OnEnable()
        {
        }


        private void Update()
        {

            //Global timer must be updated every frame
            global_timer += Time.deltaTime;


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
                    if (seen == LookedAtObject)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        timer = 0.0f;
                        LookedAtObject = seen;
                    }
                    break;
                }
                else
                {
                    if (index == GazePriority[GazePriority.Length - 1])
                    {
                        LookedAtObject = isnull;
                        timer = 0.0f;
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

    }
}
