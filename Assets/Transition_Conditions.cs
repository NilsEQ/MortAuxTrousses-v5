using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_Conditions : MonoBehaviour
{

    private GameObject Transition_Handler;
    private GameObject Speedtracker_object;


    // Start is called before the first frame update
    private bool done;

    public GameObject previousRig;
    public GameObject nextRig;


    Camera mycamera;

    public bool watch;
    public bool frame_search;
    public bool notframe_search;


    public GameObject toWatch;
    public float castwidth;

    public float delay;
    private float timer;

    // Distance to an object
    public bool checkdistance;
    public float distance;
    public GameObject toDistance;

    //Dont move

    public bool isnotmove;
    public float threshold;


    private void Start()
    {
        Transition_Handler = GameObject.Find("Transition_Handler");
        Speedtracker_object = GameObject.Find("Speedtracker");

    }

    void OnEnable()
    {
        done = false;
        timer = delay;
        mycamera = previousRig.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
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


        if (checkdistance)
        {
            Collider mycollider = toDistance.GetComponent<Collider>();
            Vector3 mypoint = mycollider.ClosestPointOnBounds(mycamera.transform.position);
            if (Vector3.Distance(mycamera.transform.position, mypoint) < distance)
            {
                Debug.Log(Vector3.Distance(mycamera.transform.position, mypoint));
                doTransition();
            }
        }

        if (isnotmove)
        {
            float ang_speed = Speedtracker_object.GetComponent<SpeedTracker>().headAngSpeed.magnitude;
            if (ang_speed < threshold)
            {
                countdown();
            }
            else
            {
                timer = delay;
            }
        }


    }

    private void OnDisable()
    {
        doTransition();
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

    private bool ICanSee()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mycamera);
        return GeometryUtility.TestPlanesAABB(planes, toWatch.GetComponent<Collider>().bounds);
    }

}


