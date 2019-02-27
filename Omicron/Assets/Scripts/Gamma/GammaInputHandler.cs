using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GammaInputHandler : MonoBehaviour
{
    private GammaLevelManager gammaManager;
    private Transform ovrRemote;                        // The ovr remote's transform
    private bool isTrapDoorTargetted;                   // Flag for it the trap door has been targetted
    private GameObject trapDoor;                        // Gameobject variable that stores a reference to the trap door interacted with

    [SerializeField] private Text debugText;
    [SerializeField] private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        gammaManager = GetComponent<GammaLevelManager>();
        ovrRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        isTrapDoorTargetted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks whether game is being run on PC or VR
        // changes input methods based ont this
        if (Application.platform == RuntimePlatform.WindowsEditor)
            PCInput();
        else
            VRInput();

        CheckIfPuzzleFailed();
        ResetPuzzle();
    }

    private void PCInput()
    {
        // Uses raycast to see if BetaBox placeable zone is being targetted when mouse button is clicked
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 15f, layerMask))
        {
            trapDoor = hit.collider.gameObject;
            gammaManager.TrapDoorOver(trapDoor);

            if (Input.GetMouseButtonDown(0) && gammaManager.IsTrapDoorOver)
            {
                gammaManager.TrapDoorSelect(trapDoor);
            }
            else if (Input.GetMouseButtonUp(0) && gammaManager.IsTrapDoorOver && gammaManager.IsTrapDoorSelected)
            {
                gammaManager.TrapDoorDeselect(trapDoor);
            }
        }
        else if (!gammaManager.IsTrapDoorSelected)
        {
            gammaManager.TrapDoorEnd(trapDoor);
            isTrapDoorTargetted = false;
        }
        else if(Input.GetMouseButtonUp(0) && gammaManager.IsTrapDoorSelected)
        {
            gammaManager.TrapDoorDeselect(trapDoor);
        }
    }

    private void VRInput()
    {
        RaycastHit hit;
        // Get the foward direciton of the remote
        Vector3 remoteDirection = ovrRemote.forward;     
        // Ge the position of the remote
        Vector3 remotePos = ovrRemote.position;
        if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, layerMask))
        {
            trapDoor = hit.collider.gameObject;
            gammaManager.TrapDoorOver(trapDoor);

            // Check if the trigger on the ovr remote has been pressed down
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote) && gammaManager.IsTrapDoorOver)
            {
                // Select the trapdoor
                gammaManager.TrapDoorSelect(trapDoor);
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote) && gammaManager.IsTrapDoorSelected)
            {
                // Deselect the trap door if the trap door is 
                gammaManager.TrapDoorDeselect(trapDoor);
            }
        }
        else if (!gammaManager.IsTrapDoorSelected)
        {
            gammaManager.TrapDoorEnd(trapDoor);
            isTrapDoorTargetted = false;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote) && gammaManager.IsTrapDoorSelected)
        {
            gammaManager.TrapDoorDeselect(trapDoor);
        }
    }

    private void CheckIfPuzzleFailed()
    {

    }

    private void ResetPuzzle()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote))
        {
            gammaManager.PuzzleReset();
        }
            else if (Input.GetKeyDown(KeyCode.E))
        {
            gammaManager.PuzzleReset();
        }
    }
}
