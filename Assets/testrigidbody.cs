using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testrigidbody : MonoBehaviour
{

    private BoxCollider bc;
    public Vector3 myvector;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        myvector = bc.center;
    }
}
