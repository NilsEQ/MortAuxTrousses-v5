using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ViveSR.anipal.Eye;




public class Data_writer : MonoBehaviour
{

    private float global_timer;
    string path;

    StreamWriter writer_Immersant;
    StreamWriter writer_Fluents;
    StreamWriter writer_Cuts;

    private Looking_tracker looktracker;
    private SpeedTracker speedtracker;
    private GameObject TrackerChest;

    Rig_Handler righandler;
    GameObject currentRig;

    // Start is called before the first frame update
    void Awake()
    {
        looktracker = GameObject.Find("EyeData").GetComponent<Looking_tracker>();
        speedtracker = GameObject.Find("Speedtracker").GetComponent<SpeedTracker>();
        TrackerChest = GameObject.Find("TrackerChest");


        path = Application.dataPath + "/Data/" + DateTime.Now.ToString("dd-MM-yy   hh-mm-ss");
        Directory.CreateDirectory(path);

        righandler = GameObject.Find("Transition_Handler").GetComponent<Rig_Handler>();
        righandler.path = path + "/pictures";
        Directory.CreateDirectory(righandler.path);


        writer_Immersant = new StreamWriter(path + "/ImmersantData.csv");
        writer_Fluents = new StreamWriter(path + "/FluentsData.csv");
        writer_Cuts = new StreamWriter(path + "/CutsData.csv");
    }

    private void Start()
    {
        currentRig = righandler.currentRig;
    }

    void onDisable()
    {
        writer_Immersant.Flush();
        writer_Immersant.Close();

        writer_Fluents.Flush();
        writer_Fluents.Close();

        writer_Cuts.Flush();
        writer_Cuts.Close();

    }

    // Update is called once per frame
    void Update()
    {
        //Global timer must be updated every frame
        global_timer += Time.deltaTime;

        GameObject seen = looktracker.eyetracked_object;
        float speed = speedtracker.headAngSpeed.magnitude;


        Vector3 position = speedtracker.headPosition;
        Ray GazeRay = looktracker.GazeRay;
        Quaternion rotation = speedtracker.headRotation;

        Vector3 ChestPos = TrackerChest.transform.position;
        Quaternion ChestRot = TrackerChest.transform.rotation;

        write_Immersant(position, rotation, GazeRay, ChestPos, ChestRot);

        if (seen != null)
        {
            write_Fluents(seen, speed);
        }

        if (currentRig != righandler.currentRig)
        {
            currentRig = righandler.currentRig;
            write_cut();
        }
    }

    private void write_Immersant(Vector3 position, Quaternion rotation, Ray GazeRay, Vector3 ChestPos, Quaternion ChestRot)
    {
        string t = global_timer.ToString().Replace(',', '.');

        string x = position.x.ToString().Replace(',', '.');
        string y = position.y.ToString().Replace(',', '.');
        string z = position.z.ToString().Replace(',', '.');

        string qx = rotation.x.ToString().Replace(',', '.');
        string qy = rotation.y.ToString().Replace(',', '.');
        string qz = rotation.z.ToString().Replace(',', '.');
        string qw = rotation.w.ToString().Replace(',', '.');

        string GOx = GazeRay.origin.x.ToString().Replace(',', '.');
        string GOy = GazeRay.origin.y.ToString().Replace(',', '.');
        string GOz = GazeRay.origin.z.ToString().Replace(',', '.');

        string GDx = GazeRay.direction.x.ToString().Replace(',', '.');
        string GDy = GazeRay.direction.y.ToString().Replace(',', '.');
        string GDz = GazeRay.direction.z.ToString().Replace(',', '.');

        string CPx = ChestPos.x.ToString().Replace(',', '.');
        string CPy = ChestPos.y.ToString().Replace(',', '.');
        string CPz = ChestPos.z.ToString().Replace(',', '.');

        string CRx = ChestRot.x.ToString().Replace(',', '.');
        string CRy = ChestRot.y.ToString().Replace(',', '.');
        string CRz = ChestRot.z.ToString().Replace(',', '.');
        string CRw = ChestRot.w.ToString().Replace(',', '.');

        writer_Immersant.WriteLine(t + "," + x + "," + y + "," + z + "," + qx + "," + qy + "," + qz + "," + qw + "," + GOx + "," + GOy + "," + GOz + "," + GDx + "," + GDy + "," + GDz + "," + CPx + "," + CPy + "," + CPz + "," + CRx + "," + CRy + "," + CRz + "," + CRw);
        writer_Immersant.Flush();
    }

    private void write_Fluents(GameObject seen, float speed)
    {
        string t = global_timer.ToString().Replace(',', '.');
        writer_Fluents.WriteLine(t + "," + seen.name + "," + speed.ToString().Replace(',', '.'));
        writer_Fluents.Flush();
    }

    private void write_cut()
    {
        Debug.Log(global_timer.ToString().Replace(',', '.') + "," + currentRig.name);
        writer_Cuts.WriteLine(global_timer.ToString().Replace(',', '.') + "," + currentRig.name);
        writer_Cuts.Flush();
    }
}
