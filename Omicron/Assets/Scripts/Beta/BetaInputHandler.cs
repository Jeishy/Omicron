using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaInputHandler : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private BetaMagnetPlacement betaMagnetPlacement;
    private Transform _ovrRemoteTrans;
    [SerializeField] private Text debugText;
    [SerializeField] private GameObject magnetPlaceVisualizerPrefab;    // Reference to grey magnet placement visualizer prefab
    [SerializeField] private float hitZPos;                             // Z component of position that magnets must be placed in

    private void Start()
    {
        betaManager = GetComponent<BetaLevelManager>();
        betaMagnetPlacement = betaManager.GetComponent<BetaMagnetPlacement>();
        _ovrRemoteTrans = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        betaMagnetPlacement = betaManager.GetComponent<BetaMagnetPlacement>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Checks whether game is being run on PC or VR
        // changes input methods based ont this
        if (Application.platform == RuntimePlatform.WindowsEditor)
            PCInput();
        else
            VRInput();
            
        MagnetsReset();
        MagnetPlaceVisualizer();
    }

    private void VRInput()
    {
        // If Oculus go remote trigger is squeezed,
        // raycast is used to see if BetaBox placeable zone is being targetted when mouse button is clicked
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            RaycastHit hit;
            Vector3 remoteDirection = _ovrRemoteTrans.forward;
            Vector3 remotePos = _ovrRemoteTrans.position;
            int layerMask = 1 << 11;                // bit shifted layer mask of MagnetPlaceable
            if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, layerMask))
            {
                //debugText.text = "Boundary Hit!";
                Vector3 hitPos = hit.point;
                Vector3 placementPos = new Vector3(hitPos.x, hitPos.y, hitZPos);    // Ensures magnets are placed on correct Z position, X and Y of hit position
                betaManager.MagnetPlaced(placementPos);                             // Places magnet at placement position
            }
        }
    }

    private void PCInput()
    {
        // Uses raycast to see if BetaBox placeable zone is being targetted when mouse button is clicked
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
        // Caches position of balls placed
        float ballsPlaced = betaMagnetPlacement.ballsPlaced;
        // If there are more than 0 balls placed,
        // and Touchpad is pressed
        // reset all balls position and remove all balls plaed
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
        Vector3 remoteDirection = _ovrRemoteTrans.forward;
        Vector3 remotePos = _ovrRemoteTrans.transform.position;
        float ballsPlaced = betaMagnetPlacement.ballsPlaced;
        float maxPlaceableMagnets = betaManager.MaxPlaceableMagnets;
        int layerMask = 1 << 11;
        // If the player is targetting the magnet placeable area, and there are more balls to place
        // then show the magnet visualizer prefab, so that player can see where they will place a magnet
        if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, layerMask) && ballsPlaced < maxPlaceableMagnets)
        {
            magnetPlaceVisualizerPrefab.SetActive(true);
            magnetPlaceVisualizerPrefab.GetComponent<Transform>().position = hit.point;
        }
        else
        {
            // Set prefab to false if magnet placeable area is not targetted
            magnetPlaceVisualizerPrefab.SetActive(false);
        }
    }
}
