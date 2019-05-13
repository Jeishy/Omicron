using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubWorldInputHandler : MonoBehaviour
{
    private HubWorldManager hubManager;
    private Transform oculusRemote;
    private Ray ray;
    private bool isTargetted;
    private Collider hitPanelCol;
    private string selectedLevel;
    private bool _isPlatformVR;

    private void Start()
    {
        oculusRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        hubManager = GetComponent<HubWorldManager>();
        isTargetted = false;
        if (Application.platform == RuntimePlatform.WindowsEditor)
            _isPlatformVR = false;
        else
            _isPlatformVR = true;
    }

    private void Update()
    {
        if (!_isPlatformVR)
            PCInput();
        else
            VRInput();
    }

    private void VRInput()
    {
        RaycastHit hit;
        if (Physics.Raycast(oculusRemote.position, oculusRemote.forward, out hit, Mathf.Infinity))
        {
            
            if (hit.collider.CompareTag("UIPanel") && isTargetted == false)
            {
                hitPanelCol = hit.collider;
                isTargetted = true;
                hubManager.Over(hit.collider);
                selectedLevel = hit.collider.name;
            }
            else if (isTargetted && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
            {
                hubManager.LevelSelect(selectedLevel);
            }
        }
        else if (isTargetted)
        {
            isTargetted = false;
            hubManager.Exit(hitPanelCol);
        }
    }

    private void PCInput()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 15f))
        {
            if (hit.collider.CompareTag("UIPanel") && isTargetted == false)
            {
                hitPanelCol = hit.collider;
                isTargetted = true;
                hubManager.Over(hitPanelCol);
                selectedLevel = hit.collider.name;
            }
            else if (isTargetted && Input.GetMouseButtonDown(0))
            {
                Debug.Log(selectedLevel);
                hubManager.LevelSelect(selectedLevel);
            }
        }
        else if (isTargetted)
        {
            isTargetted = false;
            hubManager.Exit(hitPanelCol);
        }
    }
}
