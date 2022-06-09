using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ViveSR.anipal.Eye;




public class Data_writer : MonoBehaviour
{

    private float global_timer;
    StreamWriter writer;

    // Start is called before the first frame update
    void Start()
    {
        writer = new StreamWriter(Application.dataPath + "/Data/" + DateTime.Now.ToString("dd-MM-yy   hh-mm-ss") + ".csv");
    }

    void onDisable()
    {
        writer.Flush();
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        //Global timer must be updated every frame
        global_timer += Time.deltaTime;

        GameObject seen = GameObject.Find("EyeData").GetComponent<Looking_tracker>().LookedAtObject;
        float speed = GameObject.Find("Speedtracker").GetComponent<SpeedTracker>().headAngSpeed.magnitude;
        if (seen != null)
        {
            write_data(seen,speed);

        }
    }


    private void write_data(GameObject seen, float speed)
    {
        writer.WriteLine(global_timer.ToString().Replace(',', '.') + "," + seen.name + "," + speed.ToString().Replace(',', '.'));
    }
}
