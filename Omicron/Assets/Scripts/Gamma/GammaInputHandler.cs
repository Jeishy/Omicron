using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GammaInputHandler : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    private Transform _ovrRemote;                        // The ovr remote's transform
    private bool _isTrapDoorTargetted;                   // Flag for it the trap door has been targetted
    private GameObject _trapDoor;                        // Gameobject variable that stores a reference to the trap door interacted with
    private IEnumerator _waitToRestartPuzzleCoroutine;
    private bool _isPuzzleRestarted;


    [SerializeField] private Text debugText;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _timeToRestartPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
        _ovrRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        _isTrapDoorTargetted = false;
        _waitToRestartPuzzleCoroutine = WaitToRestartPuzzle();
        _isPuzzleRestarted = false;
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

        if (!_isPuzzleRestarted)
            StartCoroutine(WaitToCheckPuzzleRestart());
    }

    private void PCInput()
    {
        // Uses raycast to see if BetaBox placeable zone is being targetted when mouse button is clicked
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 15f, layerMask))
        {
            _trapDoor = hit.collider.gameObject;
            _gammaManager.TrapDoorOver(_trapDoor);

            if (Input.GetMouseButtonDown(0) && _gammaManager.IsTrapDoorOver)
            {
                _gammaManager.TrapDoorSelect(_trapDoor);
            }
            else if (Input.GetMouseButtonUp(0) && _gammaManager.IsTrapDoorOver && _gammaManager.IsTrapDoorSelected)
            {
                _gammaManager.TrapDoorDeselect(_trapDoor);
            }
        }
        else if (!_gammaManager.IsTrapDoorSelected)
        {
            _gammaManager.TrapDoorEnd(_trapDoor);
            _isTrapDoorTargetted = false;
        }
        else if(Input.GetMouseButtonUp(0) && _gammaManager.IsTrapDoorSelected)
        {
            _gammaManager.TrapDoorDeselect(_trapDoor);
        }
    }

    private void VRInput()
    {
        RaycastHit hit;
        // Get the foward direciton of the remote
        Vector3 remoteDirection = _ovrRemote.forward;     
        // Ge the position of the remote
        Vector3 remotePos = _ovrRemote.position;
        if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, layerMask))
        {
            _trapDoor = hit.collider.gameObject;
            _gammaManager.TrapDoorOver(_trapDoor);

            // Check if the trigger on the ovr remote has been pressed down
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote) && _gammaManager.IsTrapDoorOver)
            {
                // Select the trapdoor
                _gammaManager.TrapDoorSelect(_trapDoor);
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote) && _gammaManager.IsTrapDoorSelected)
            {
                // Deselect the trap door if the trap door is 
                _gammaManager.TrapDoorDeselect(_trapDoor);
            }
        }
        else if (!_gammaManager.IsTrapDoorSelected)
        {
            _gammaManager.TrapDoorEnd(_trapDoor);
            _isTrapDoorTargetted = false;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote) && _gammaManager.IsTrapDoorSelected)
        {
            _gammaManager.TrapDoorDeselect(_trapDoor);
        }
    }

    private void CheckIfPuzzleFailed()
    {
        if (_gammaManager.ColdParticlesInPuzzle == 0 || _gammaManager.HotParticlesInPuzzle == 0 )
        {
            _isPuzzleRestarted = true;
            StopCoroutine(_waitToRestartPuzzleCoroutine);
            StartCoroutine(WaitToRestartPuzzle());
        }
    }

    private void RestartPuzzle()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote))
        {
            _isPuzzleRestarted = true;
            StopCoroutine(_waitToRestartPuzzleCoroutine);
            // Play Fade out and in animation
            StartCoroutine(WaitToRestartPuzzle());
        }
            else if (Input.GetKeyDown(KeyCode.E))
        {
            _isPuzzleRestarted = true;
            StopCoroutine(_waitToRestartPuzzleCoroutine);
            StartCoroutine(WaitToRestartPuzzle());
        }
    }

    private IEnumerator WaitToRestartPuzzle()
    {
        yield return new WaitForSeconds(_timeToRestartPuzzle);
        _gammaManager.PuzzleRestart();
        _isPuzzleRestarted = false;
    }

    private IEnumerator WaitToCheckPuzzleRestart()
    {
        yield return new WaitForSeconds(1f);
        CheckIfPuzzleFailed();
        RestartPuzzle();
    }
}
