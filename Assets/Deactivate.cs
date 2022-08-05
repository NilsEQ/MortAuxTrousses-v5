using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        int n = transform.childCount;

        int i = 0;

        while (i < n)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
