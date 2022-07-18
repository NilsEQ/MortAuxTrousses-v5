using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[ExecuteInEditMode]
public class FindStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Object[] mylist = Object.FindObjectsOfType(typeof(PlayableDirector));

        for (int i = 0; i < (mylist.Length); i++)
        {
            Debug.Log(mylist[i]);

        }

    }


}
