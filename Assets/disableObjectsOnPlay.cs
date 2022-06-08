using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableObjectsOnPlay : MonoBehaviour
{
    public GameObject[] objectsToDisactivate;
    void Start()
    {
        for (int i = 0; i < objectsToDisactivate.Length; i++)
        {
            objectsToDisactivate[i].SetActive(false);
        }

    }

}
