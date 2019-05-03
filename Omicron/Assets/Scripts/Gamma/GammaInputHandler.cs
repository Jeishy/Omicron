using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GammaInputHandler : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    private bool _isTrapDoorTargetted;                   // Flag for it the trap door has been targetted
    private GameObject _trapDoor;                        // Gameobject variable that stores a reference to the trap door interacted with
    private IEnumerator _waitToRestartPuzzleCoroutine;
    private bool _isPuzzleRestarted;
    private bool _isPlatformVR;
    private bool _canPuzzleStateBeChecked;


    [SerializeField] private Text debugText;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _timeToRestartPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
        _isTrapDoorTargetted = false;
        _waitToRestartPuzzleCoroutine = WaitToRestartPuzzle();
        _isPuzzleRestarted = false;
        _canPuzzleStateBeChecked = false;
        StartCoroutine(WaitToCheckPuzzleState());
        // Checks whether game is being run on PC or VR
        // changes input methods based ont this
        if (Application.platform == RuntimePlatform.WindowsEditor)
            _isPlatformVR = false;
        else
            _isPlatformVR = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlatformVR)
            VRInput();
        else
            PCInput();

        if (!_isPuzzleRestarted)
        {
            if (_canPuzzleStateBeChecked)
                CheckIfPuzzleFailed();
            RestartPuzzle();
        }
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
        Vector3 remoteDirection = GameManager.Instance.OVRRemote.forward;     
        // Ge the position of the remote
        Vector3 remotePos = GameManager.Instance.OVRRemote.position;
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
        Debug.Log("Size of hot particles list is " + _gammaManager.HotParticlesInPuzzle.Count);
        Debug.Log("Size of cold particles list is " + _gammaManager.ColdParticlesInPuzzle.Count);
        if (_gammaManager.ColdParticlesInPuzzle.Count == 0 || _gammaManager.HotParticlesInPuzzle.Count == 0 )
        {
            Debug.Log("Restarting puzzle");
            _isPuzzleRestarted = true;
            StartCoroutine(WaitToRestartPuzzle());
        }
    }

    private void RestartPuzzle()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote))
        {
            _isPuzzleRestarted = true;
            NormalRestartPuzzle();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _isPuzzleRestarted = true;
            NormalRestartPuzzle();
        }
    }

    private IEnumerator WaitToRestartPuzzle()
    {
        // Puzzle has been failed, so restart the puzzle
        yield return new WaitForSeconds(_timeToRestartPuzzle);
        _gammaManager.PuzzleRestart();
        _isPuzzleRestarted = false;
    }

    private void NormalRestartPuzzle()
    {
        _gammaManager.PuzzleRestart();
        _isPuzzleRestarted = false;
    }

    private IEnumerator WaitToCheckPuzzleState()
    {
        yield return new WaitForSeconds(2f);
        _canPuzzleStateBeChecked = true;
    }
}
