using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;
using ViveSR.anipal.Eye;
using UnityEngine.Playables;

public class ReplayScript : MonoBehaviour
{
    public float timer;
    public string Repository = "05-07-22   02-45-33 - Aurianne";
    private string path;


    List<String> CutsData;
    List<String> ImmersantData;

    private string[] currentdatasImmersant;
    private string[] nextdatasImmersant;
    private int i_Immersant;

    private string[] currentdatasCuts;
    private string[] nextdatasCuts;
    private int i_Cuts;


    public GameObject currentRig;
    private Transform currentCamera;
    private Transform BodyMoving;

    public PlayableDirector Director;

    CultureInfo ci;

    GameObject Rigs;


    private lastCutData lastCutData;

    float MaxDistance = 20;
    public GameObject SeenObject;
    GameObject isnull;

    // Start is called before the first frame update
    void Start()
    {
        ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.NumberDecimalSeparator = ".";

        path = Application.dataPath + "/Data/" + Repository;
        CutsData = new List<string>();
        ImmersantData = new List<string>();
        storeData(CutsData,path + "/CutsData.csv", "0," + currentRig.name);
        storeData(ImmersantData, path + "/ImmersantData.csv", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0") ;

        i_Immersant = 0;
        currentdatasImmersant = ImmersantData[0].Split(",");
        nextdatasImmersant = ImmersantData[1].Split(",");

        i_Cuts = 0;
        currentdatasCuts = CutsData[0].Split(",");
        nextdatasCuts = CutsData[1].Split(",");

        currentRig.SetActive(true);
        currentCamera = currentRig.transform.Find("Camera");
        BodyMoving = currentRig.transform.Find("BodyMoving");


        Rigs = GameObject.Find("RIGS");
        lastCutData = GameObject.Find("lastCutData").GetComponent<lastCutData>();

        isnull = new GameObject("Nullreference");
    }

    // Update is called once per frame
    void Update()
    {
        timer = (float)Director.time;

        int mem = i_Immersant;
        while (timer >= float.Parse(nextdatasImmersant[0], ci))
        {
            i_Immersant += 1;
            currentdatasImmersant = ImmersantData[i_Immersant].Split(",");
            nextdatasImmersant = ImmersantData[i_Immersant + 1].Split(",");
        }
        while (timer < float.Parse(currentdatasImmersant[0], ci))
        {
            i_Immersant -= 1;
            currentdatasImmersant = ImmersantData[i_Immersant].Split(",");
            nextdatasImmersant = ImmersantData[i_Immersant + 1].Split(",");
        }
        if (i_Immersant != mem)
        {
            UpdateImmersant();
        }


        mem = i_Cuts;
        while (timer >= float.Parse(nextdatasCuts[0], ci))
        {
            i_Cuts += 1;
            currentdatasCuts = CutsData[i_Cuts].Split(",");
            nextdatasCuts = CutsData[i_Cuts + 1].Split(",");
        }
        while (timer < float.Parse(currentdatasCuts[0], ci))
        {
            i_Cuts -= 1;
            currentdatasCuts = CutsData[i_Cuts].Split(",");
            nextdatasCuts = CutsData[i_Cuts + 1].Split(",");
        }
        if (i_Cuts != mem)
        {
            UpdateRig();
        }

        Vector3 beginning = currentCamera.transform.position;
        Vector3 direction = new Vector3(float.Parse(currentdatasImmersant[11], ci), float.Parse(currentdatasImmersant[12], ci), float.Parse(currentdatasImmersant[13], ci));
        Vector3 end = beginning + MaxDistance * currentCamera.transform.TransformDirection(direction);
        Ray rayGlobal = new Ray(currentCamera.transform.position, currentCamera.transform.TransformDirection(direction));


        Debug.DrawLine(beginning, end, Color.red);

        RaycastHit hit;
        bool valid = Physics.Raycast(rayGlobal, out hit, MaxDistance);

        if (valid)
        {
            SeenObject = hit.transform.gameObject;
        }
        else
        {
            SeenObject = isnull;
        }
    }

    void UpdateImmersant()
    {
        currentCamera.localPosition = new Vector3(float.Parse(currentdatasImmersant[1], ci), float.Parse(currentdatasImmersant[2], ci), float.Parse(currentdatasImmersant[3], ci));
        currentCamera.localRotation = new Quaternion(float.Parse(currentdatasImmersant[4], ci), float.Parse(currentdatasImmersant[5], ci), float.Parse(currentdatasImmersant[6], ci), float.Parse(currentdatasImmersant[7], ci));

        BodyMoving.localPosition = new Vector3(float.Parse(currentdatasImmersant[14], ci), float.Parse(currentdatasImmersant[15], ci), float.Parse(currentdatasImmersant[16], ci));
        BodyMoving.localRotation = new Quaternion(float.Parse(currentdatasImmersant[17], ci), float.Parse(currentdatasImmersant[18], ci), float.Parse(currentdatasImmersant[19], ci), float.Parse(currentdatasImmersant[20], ci));


    }


    void UpdateRig()
    {
        float cutTime = float.Parse(currentdatasCuts[0], ci);

        int i = 0;
        string[] immersantAtCut = ImmersantData[i].Split(",");
        while(cutTime > float.Parse(immersantAtCut[0], ci))
        {
            i += 1;
            immersantAtCut = ImmersantData[i].Split(",");
        }
        lastCutData.position = new Vector3(float.Parse(immersantAtCut[1], ci), float.Parse(immersantAtCut[2], ci), float.Parse(immersantAtCut[3], ci));
        lastCutData.rotation = new Quaternion(float.Parse(immersantAtCut[17], ci), float.Parse(immersantAtCut[18], ci), float.Parse(immersantAtCut[19], ci), float.Parse(immersantAtCut[20], ci));


        Vector3 pos = currentCamera.localPosition;
        Quaternion rot = currentCamera.localRotation;

        Vector3 BodPos = BodyMoving.localPosition;
        Quaternion BodRot = BodyMoving.localRotation;


        currentRig.SetActive(false);
        currentRig = FindDeepChild(Rigs, currentdatasCuts[1]);
        currentRig.SetActive(true);

        currentCamera = currentRig.transform.Find("Camera");
        currentCamera.localPosition = pos;
        currentCamera.localRotation = rot;

        BodyMoving = currentRig.transform.Find("BodyMoving");
        BodyMoving.localPosition = BodPos;
        BodyMoving.localRotation = BodRot;
    }

    //Breadth-first search
    public static GameObject FindDeepChild(GameObject aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent.transform);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            Debug.Log(c.name);
            if (c.name == aName)
                return c.gameObject;
            foreach (Transform t in c)
            {
                queue.Enqueue(t);
            }
        }
        return null;
    }

    public void storeData(List<String> dataList, String file, String BeginningData)
    {
        dataList.Add(BeginningData);
        StreamReader Data = new StreamReader(file);

        String line = Data.ReadLine();

        while (line != null)
        {
            dataList.Add(line);
            line = Data.ReadLine();
        }
    }
}
