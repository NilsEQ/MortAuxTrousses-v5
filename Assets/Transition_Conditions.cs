using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class Transition_Conditions : MonoBehaviour
{

    private GameObject Transition_Handler;
    private Rig_Handler Rig_Handler;


    // Start is called before the first frame update

    //A parameter called in order to not do a transition when the scene begins (a weird quirk of Unity)
    private bool isfirsttime;

    //Our rigs
    public GameObject previousRig;
    public GameObject nextRig;

    Camera mycamera;

    //Condition : not moving
    public bool isnotmove;
    public float speed_threshold = 0.2f;
    public float delay_notmove;
    private Speed_Tresh Speed_script;

    //Condition : Eye tracking
    public bool eyetracking;
    public GameObject eyetracked_object;
    public float eyetrack_delay;
    private Looking_tracker lookingtracker;


    public bool watch;
    public bool frame_search;
    public bool notframe_search;


    public GameObject toWatch;
    public float castwidth;

    public float delay;
    private float timer;

    // Checking distance to an object
    public bool checkdistance;
    public float distance;
    public GameObject toDistance;
    public float distance_delay;
    private Distance_Checker DistanceChecker;






    private void Awake()
    {
        isfirsttime = true;

        Transition_Handler = GameObject.Find("Transition_Handler");
        Rig_Handler = Transition_Handler.GetComponent<Rig_Handler>();


        if (eyetracking)
        {
            lookingtracker = GameObject.Find("EyeData").GetComponent<Looking_tracker>();
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
            GameObject Speedtracker = GameObject.Find("Speedtracker");
            Speed_script = Speedtracker.AddComponent<Speed_Tresh>();
            Speed_script.threshold = speed_threshold;
        }

    }

    private void Start()
    {

    }


    void OnEnable()
    {
        timer = delay;
        mycamera = previousRig.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        bool dewit = true;
        
        if (isnotmove)
        {
            dewit = dewit && (Speed_script.timer > delay_notmove);
        }

        if (eyetracking)
        {
            dewit = dewit && (lookingtracker.eyetracked_object == eyetracked_object && lookingtracker.timer > eyetrack_delay);
        }

        if (checkdistance)
        {
            dewit = dewit && (DistanceChecker.timer > delay);
        }


        if (watch)
        {
            Transform transform = previousRig.transform.GetChild(2);
            Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 200, Color.red);
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);

            if (Physics.SphereCast(ray, castwidth, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject == toWatch)
                {
                    countdown();
                }
                else
                {
                    timer = delay;
                }
            }
            else
            {
                timer = delay;
            }
        }


        if (frame_search)
        {
            if (ICanSee())
            {
                countdown();
            }
            else
            {
                timer = delay;
            }
        }

        if (notframe_search)
        {
            if (!ICanSee())
            {
                countdown();
            }
            else
            {
                timer = delay;
            }
        }

        if (dewit && !isfirsttime) doTransition();
    }

    private void OnDisable()
    {
        if (!isfirsttime)
        {
            doTransition();
        }
        else
        {
            isfirsttime = false;
        }
    }

    private void doTransition()
    {
        if (Rig_Handler.currentRig == previousRig)
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/" + previousRig.name + " to " + nextRig.name + " - Before.png");
            Rig_Handler.nextRig = nextRig;
            Rig_Handler.transition = true;
        }

    }

    private GameObject createTrackingObject(GameObject myprefab)
    {
        GameObject newobject = Instantiate(myprefab);
        return newobject;
    }


    private void countdown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            doTransition();
        }
    }

    private bool ICanSee()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mycamera);
        return GeometryUtility.TestPlanesAABB(planes, toWatch.GetComponent<Collider>().bounds);
    }

}


