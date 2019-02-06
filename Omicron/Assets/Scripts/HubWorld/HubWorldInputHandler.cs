using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldInputHandler : MonoBehaviour
{
    private HubWorldManager hubManager;
    private Transform oculusRemote;
    private int layerMask;

    private void Start()
    {
        oculusRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        hubManager = GetComponent<HubWorldManager>();
        layerMask = 1 << 12;
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            Raycast();
        }
    }

    private void Raycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(oculusRemote.position, oculusRemote.forward, out hit, Mathf.Infinity, layerMask))
        {
            string selectedLevel = hit.collider.name;
            hubManager.LevelSelect(selectedLevel);
        }
    }
}
