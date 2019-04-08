using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaPlayerTeleport : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;              // Layer mask of targettable area
    [SerializeField] private Transform _ovrCameraTrans;         // Parent gameobject of the OVRCameraRig

    private Transform _ovrRemoteTrans;                          // Remote transform

    private void Start()
    {
        _ovrRemoteTrans = GameObject.FindGameObjectWithTag("OculusRemote").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if remote trigger is pressed
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            // Teleport the player
            Teleport();
        }
    }

    private void Teleport()
    {
        RaycastHit hit;
        // Get remotes forward direction and current position
        Vector3 remoteDirection = _ovrRemoteTrans.forward;
        Vector3 remotePos = _ovrRemoteTrans.position;
        // Do a raycast to see if you are targetting an area you can teleport to
        if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, _layerMask))
        {
            // Set new position along the x and y axis
            _ovrCameraTrans.position = new Vector3(hit.point.x, _ovrCameraTrans.position.y, hit.point.z);
        }
    }
}