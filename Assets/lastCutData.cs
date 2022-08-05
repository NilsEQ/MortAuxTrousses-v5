using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastCutData : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(0, 0, 0);
        rotation = new Quaternion(0, 0, 0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
