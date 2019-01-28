using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaInputHandler : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private Transform ovrRemote;
    [SerializeField] private Text debugText;
    [SerializeField] private Transform centerCameraAnchorTrans;
    // Start is called before the first frame update
    private void Start()
    {
        betaManager = GetComponent<BetaLevelManager>();
        ovrRemote = GameObject.Find("OculusGoControllerModel").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        PCInput();
    }

    private void VRInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            Debug.Log("Magnet placing commenced.");
            RaycastHit hit;
            Vector3 remoteDirection = ovrRemote.forward;
            Vector3 remotePos = ovrRemote.transform.position;
            int layerMask = 1 << 11;
            if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, layerMask))
            {
                //debugText.text = "Boundary Hit!";
                Vector3 hitPos = hit.point;
                Vector3 placementPos = new Vector3(hitPos.x, hitPos.y, 4.2f);
                betaManager.MagnetPlaced(placementPos);
            }
        }
    }

    private void PCInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Raycast intiated");                
                Vector3 hitPos = hit.point;
                Vector3 placementPos = new Vector3(hitPos.x, hitPos.y, 4.2f);
                betaManager.MagnetPlaced(placementPos);
            }
        }
    }
}
