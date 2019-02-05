using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script caches references to objects using expensive GameObject.Find calls at a point that isnt detremental to gameplay
// This allows all classes to get access to GO's that dont persist across the game using a singleton pattern
public class PlayerControllerReferences : MonoBehaviour
{
    private Transform ballSpawnPoint;
    public Transform BallSpawnPoint
    {
        get { return ballSpawnPoint; }
    }

    private Transform oculusRemoteTransform;
    public Transform OculusRemoteTransform
    {
        get { return oculusRemoteTransform; }
    }

    private Transform ovrCameraRigTrans;
    public Transform OVRCameraRigTrans
    {
        get {return ovrCameraRigTrans;}
    }

    public static PlayerControllerReferences Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        ballSpawnPoint = GameObject.Find("BallSpawnPoint").transform;
        oculusRemoteTransform = GameObject.Find("OculusGoControllerModel").transform;
        ovrCameraRigTrans = GameObject.Find("OVRCameraRig").transform;
    }
}
