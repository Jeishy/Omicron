using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaInputHandler : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private BetaMagnetPlacement betaMagnetPlacement;
    private Transform ovrRemote;
    [SerializeField] private Text debugText;
    [SerializeField] private Transform centerCameraAnchorTrans;
    [SerializeField] private GameObject magnetPlaceVisualizerPrefab;
    [SerializeField] private float hitZPos;
    // Start is called before the first frame update
    private void Start()
    {
        betaManager = GetComponent<BetaLevelManager>();
        betaMagnetPlacement = betaManager.GetComponent<BetaMagnetPlacement>();
        ovrRemote = GameObject.Find("OculusGoControllerModel").GetComponent<Transform>();
        betaMagnetPlacement = betaManager.GetComponent<BetaMagnetPlacement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            PCInput();
        else
            VRInput();
            
        MagnetsReset();
        MagnetPlaceVisualizer();
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
                Vector3 placementPos = new Vector3(hitPos.x, hitPos.y, hitZPos);
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
                Vector3 hitPos = hit.point;
                Vector3 placementPos = new Vector3(hitPos.x, hitPos.y, hitZPos);
                betaManager.MagnetPlaced(placementPos);
            }
        }
    }
    
    private void MagnetsReset()
    {
        float ballsPlaced = betaMagnetPlacement.ballsPlaced;
        if (ballsPlaced > 0)
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote))
            {
                betaManager.ResetMagnets();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                betaManager.ResetMagnets();
            }
        }
    }
    
    private void MagnetPlaceVisualizer()
    {
        RaycastHit hit;
        Vector3 remoteDirection = ovrRemote.forward;
        Vector3 remotePos = ovrRemote.transform.position;
        float ballsPlaced = betaMagnetPlacement.ballsPlaced;
        float maxPlaceableMagnets = betaManager.MaxPlaceableMagnets;
        int layerMask = 1 << 11;
        if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, layerMask) && ballsPlaced < maxPlaceableMagnets)
        {
            magnetPlaceVisualizerPrefab.SetActive(true);
            magnetPlaceVisualizerPrefab.GetComponent<Transform>().position = hit.point;
        }
        else
        {
            magnetPlaceVisualizerPrefab.SetActive(false);
        }
    }
}
