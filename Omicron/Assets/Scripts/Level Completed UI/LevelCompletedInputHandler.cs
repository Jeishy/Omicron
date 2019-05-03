using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedInputHandler : MonoBehaviour
{
    [SerializeField] private LevelCompletedManager _levelCompletedManager;

    private Transform _oculusRemote;
    private Ray _ray;
    private bool _isTargetted;
    private Collider _hitPanelCol;

    private void Start()
    {
        _oculusRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        _isTargetted = false;
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
        if (Physics.Raycast(_oculusRemote.position, _oculusRemote.forward, out hit, Mathf.Infinity))
        { 
            if (hit.collider.CompareTag("UIPanel") && _isTargetted == false)
            {
                _hitPanelCol = hit.collider;
                _isTargetted = true;
                _levelCompletedManager.Over(_hitPanelCol);
            }
            else if (_isTargetted && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
            {
                _levelCompletedManager.Select(_hitPanelCol);
            }
        }
        else if (_isTargetted)
        {
            _isTargetted = false;
            _levelCompletedManager.Exit(_hitPanelCol);
        }
    }

    private void PCInput()
    {
        RaycastHit hit;
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out hit, 100f))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("UIPanel") && _isTargetted == false)
            {
                _hitPanelCol = hit.collider;
                _isTargetted = true;
                _levelCompletedManager.Over(_hitPanelCol);
            }
            else if (_isTargetted && Input.GetMouseButtonDown(0))
            {
                _levelCompletedManager.Select(_hitPanelCol);
            }
        }
        else if (_isTargetted)
        {
            _isTargetted = false;
            _levelCompletedManager.Exit(_hitPanelCol);
        }
    }
}
