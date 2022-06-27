using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Visualiser))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor
{

    public override void OnInspectorGUI()
    {

        Visualiser myVis = (Visualiser)target;

        if (GUILayout.Button("Update"))
        {
            myVis.UpdateVis();
        }
    }
}
