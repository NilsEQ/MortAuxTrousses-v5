using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class Transition_Conditions : MonoBehaviour
{

    //private GameObject Transition_Handler;
    //private Rig_Handler Rig_Handler;


    //// Start is called before the first frame update

    ////A parameter called in order to not do a transition when the scene begins (a weird quirk of Unity)
    //private bool isfirsttime;

    ////Our rigs
    //public GameObject previousRig;
    //public GameObject nextRig;

    //Condition : not moving
    public bool isnotmove;
    public float speed_threshold = 0.2f;
    public float notmove_delay;
    private Speed_Tresh Speed_script;

    //Condition : Eye tracking
    public bool eyetracking;
    public GameObject eyetracked_object;
    public float eyetrack_delay;
    private Looking_tracker lookingtracker;

    //Condition : Checking if object has been seen before cutting
    public bool hasseen_condition;
    public GameObject hasseen_object;
    public float delay_hasseen;
    private HasSeenObject hasseen_script;

    //Condition : Checking if in frame
    public bool inframe_search;
    public GameObject tocheckinframe;
    public float inframe_delay;
    private InFrameTimer InFrame_Script;


    //Condition : Checking if not in frame
    public bool notinframe_search;
    public GameObject tochecknotinframe;
    public float notinframe_delay;
    private NotinFrameTimer NotinFrame_Script;


    //Condition : Checking distance to an object
    public bool checkdistance;
    public float distance;
    public GameObject toDistance;
    public float distance_delay;
    private Distance_Checker DistanceChecker;

    //Condition : Checking distance to an object
    public bool checkheight;
    public float height_threshold = 1.0f;
    public float height_delay;
    private Height_Checker HeightChecker;

    //Deprecated : raycast from head
    //public bool watch;
    //public float castwidth;
    //public bool delay;



    private void Awake()
    {

        if (eyetracking)
        {
            lookingtracker = GameObject.Find("EyeData").GetComponent<Looking_tracker>();
        }

        if (hasseen_condition)
        {
            GameObject EyeData = GameObject.Find("EyeData");
            hasseen_script = EyeData.AddComponent<HasSeenObject>();
            hasseen_script.to_see = hasseen_object;
        }

        if (checkdistance)
        {
            GameObject Distance_Handler = GameObject.Find("Distance_Handler");
            DistanceChecker = Distance_Handler.AddComponent<Distance_Checker>();
            DistanceChecker.threshold_dist = distance;
            DistanceChecker.toDistance = toDistance;
        }

        if (isnotmove)
        {
            bool isalreadyhere = false;
            GameObject Speedtracker = GameObject.Find("Speedtracker");
            Speed_Tresh[] mylist = Speedtracker.GetComponentsInChildren<Speed_Tresh>();

            int i = 0;
            int length = mylist.Length;
            while (i < length && !isalreadyhere)
            {
                if (mylist[i].threshold == speed_threshold) {
                    Speed_script = mylist[i];
                    isalreadyhere = true;
                }
            }

            if (!isalreadyhere)
            {
                Speed_script = Speedtracker.AddComponent<Speed_Tresh>();
                Speed_script.threshold = speed_threshold;
            }
        }

        if (inframe_search)
        {
            GameObject Frame_Handler = GameObject.Find("Frame_Handler");
            InFrame_Script = Frame_Handler.AddComponent<InFrameTimer>();
            InFrame_Script.toDetect = tocheckinframe;
        }

        if (notinframe_search)
        {
            GameObject Frame_Handler = GameObject.Find("Frame_Handler");
            NotinFrame_Script = Frame_Handler.AddComponent<NotinFrameTimer>();
            NotinFrame_Script.toDetect = tochecknotinframe;
        }

        if (checkheight)
        {
            bool isalreadyhere = false;
            GameObject Speedtracker = GameObject.Find("Speedtracker");
            Height_Checker[] mylist = Speedtracker.GetComponentsInChildren<Height_Checker>();

            int i = 0;
            int length = mylist.Length;
            while (i < length && !isalreadyhere)
            {
                if (mylist[i].threshold == height_threshold)
                {
                    HeightChecker = mylist[i];
                    isalreadyhere = true;
                }
            }

            if (!isalreadyhere)
            {
                HeightChecker = Speedtracker.AddComponent<Height_Checker>();
                HeightChecker.threshold = height_threshold;
            }

        }

    }



    // Update is called once per frame
    public bool verify()
    {

        bool dewit = true;
        
        if (isnotmove)
        {
            dewit = dewit && (Speed_script.timer > notmove_delay);
        }

        if (eyetracking)
        {
            dewit = dewit && (lookingtracker.eyetracked_object == eyetracked_object && lookingtracker.timer > eyetrack_delay);
        }

        if (hasseen_condition)
        {
            dewit = dewit && (hasseen_script.timer > delay_hasseen);
        }


        if (checkdistance)
        {
            dewit = dewit && (DistanceChecker.timer > distance_delay);
        }


        //if (watch)
        //{
        //    Transform transform = previousRig.transform.GetChild(2);
        //    Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 200, Color.red);
        //    RaycastHit hit;
        //    Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);

        //    if (Physics.SphereCast(ray, castwidth, out hit, Mathf.Infinity))
        //    {
        //        if (hit.transform.gameObject == toWatch)
        //        {
        //            countdown();
        //        }
        //        else
        //        {
        //            timer = delay;
        //        }
        //    }
        //    else
        //    {
        //        timer = delay;
        //    }
        //}


        if (inframe_search)
        {
            dewit = dewit && (InFrame_Script.timer > inframe_delay);
        }

        if (notinframe_search)
        {
            dewit = dewit && (NotinFrame_Script.timer > notinframe_delay);
        }

        if (checkheight)
        {
            dewit = dewit && (HeightChecker.timer > height_delay);
        }

        return dewit;
    }


    private GameObject createTrackingObject(GameObject myprefab)
    {
        GameObject newobject = Instantiate(myprefab);
        return newobject;
    }

    //// Deprecated : methods not in this script anymore

    //private void countdown()
    //{
    //    timer -= Time.deltaTime;
    //    if (timer <= 0f)
    //    {
    //        doTransition();
    //    }
    //}

    //private bool ICanSee()
    //{
    //    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mycamera);
    //    return GeometryUtility.TestPlanesAABB(planes, toWatch.GetComponent<Collider>().bounds);
    //}

    public void relax_move(bool mybool)
    {
        isnotmove = mybool;
    }

    public void relax_eyetracking(bool mybool)
    {
        eyetracking = mybool;

    }

    public void relaxmove_inframe(bool mybool)
    {

        inframe_search = mybool;

    }

    public void relax_notinframe(bool mybool)
    {
        notinframe_search = mybool;
    }

    public void relax_distance(bool mybool)
    {
        checkdistance = mybool;
    }
}


