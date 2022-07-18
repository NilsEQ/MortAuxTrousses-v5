using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofWhenDown : MonoBehaviour
{

    public float thres = 0.7f;
    Camera mycamera;
    bool isdone;

    public const float floor = 0.6403f;
    public const float scale = 0.07f;

    // Start is called before the first frame update
    void Start()
    {
        mycamera = GetComponentInChildren<Camera>();
        isdone = false;
    }

    // Update is called once per frame
    void Update()
    {
        float height = mycamera.transform.position.y;
        
        if (height < thres && !isdone)
        {
            poof();
            isdone = true;
        }
    }

    void poof()
    {
        transform.localScale = new Vector3(scale,scale,scale) ;
        Vector3 p = transform.position;
        p.y = floor;
        transform.position = p;

    }
}
