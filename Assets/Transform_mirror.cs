using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_mirror : MonoBehaviour
{

    public Collider mycollider;
    public float threshold;

    Camera mycamera;
    bool isdone;

    public float scale_factor = 2.38f / 0.7f;
    public float translation_distance = 0.4f;
    public float enfoncement_factor = 0.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        mycamera = GetComponentInChildren<Camera>(true);
        isdone = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mypoint = mycollider.ClosestPointOnBounds(mycamera.transform.position);
        float dist = Vector3.Distance(mycamera.transform.position, mypoint);

        if (dist < threshold && !isdone)
        {
            mirror();
            isdone = true;
        }
    }

    void mirror()
    {
        Vector3 direction = mycamera.transform.rotation * Vector3.forward;
        direction.y = 0;
        direction = direction.normalized;


        transform.position += direction * translation_distance;
        transform.Rotate(Vector3.up, 180);
        transform.localScale *= scale_factor;

        //Debug.Log(transform.localPosition);


        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - enfoncement_factor * transform.localScale.x, transform.localPosition.z);
    }

}
