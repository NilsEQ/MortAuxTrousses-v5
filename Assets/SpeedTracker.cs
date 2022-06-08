using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class SpeedTracker : MonoBehaviour
{

    public Vector3 headPosition;
    public Vector3 headSpeed;
    public Vector3 headAcceleration;

    public Quaternion headRotation;
    public Vector3 headAngSpeed;
    public Vector3 headAngAcc;

    private List<XRNodeState> nodeStates = new List<XRNodeState>();
    XRNodeState headState;




    // Start is called before the first frame update
    void Start()
    {
        InputTracking.GetNodeStates(nodeStates);
        foreach (var node in nodeStates)
        {
            if(node.nodeType == XRNode.Head)
            {
                headState = node;
                break;
            }
        }    
    }

        // Update is called once per frame
        void Update()
        {


         InputTracking.GetNodeStates(nodeStates);
         foreach (var node in nodeStates)
         {
             if (node.nodeType == XRNode.Head)
             {
                 headState = node;
             }
         }


        headState.TryGetPosition(out headPosition);
        headState.TryGetVelocity(out headSpeed);
        headState.TryGetAcceleration(out headAcceleration);
        headState.TryGetRotation(out headRotation);
        headState.TryGetAngularVelocity(out headAngSpeed);
        headState.TryGetAngularAcceleration(out headAngAcc);
    }
}
