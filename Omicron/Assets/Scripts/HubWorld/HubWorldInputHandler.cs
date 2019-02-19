using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubWorldInputHandler : MonoBehaviour
{
    private HubWorldManager hubManager;
    private Transform oculusRemote;
    private int layerMask;
    private Ray ray;
    [SerializeField] private Text debugText;
    private bool isTargetted;
    private Collider hitPanelCol;
    private string selectedLevel;


    private void Start()
    {
        oculusRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        hubManager = GetComponent<HubWorldManager>();
        layerMask = 1 << 12;
        isTargetted = false;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
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
                debugText.text = "Remote Trigger Activated";
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
                //hubManager.Over(hitPanelCol);
                Debug.Log("Sup");
                selectedLevel = hit.collider.name;
            }
            else if (isTargetted && Input.GetMouseButtonDown(0))
            {
                Debug.Log(selectedLevel);
                hubManager.LevelSelect(selectedLevel);
            }
        }
        /*else if (isTargetted)
        {
            isTargetted = false;
            hubManager.Exit(hitPanelCol);
        }*/
    }
}
