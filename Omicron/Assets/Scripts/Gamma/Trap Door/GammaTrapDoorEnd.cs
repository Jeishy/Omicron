using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaTrapDoorEnd : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    private GammaTrapDoorOver _gammaTrapDoorOver;

    private void OnEnable()
    {
        Setup();
        _gammaManager.OnTrapDoorEnd += TrapDoorEnd;
    }

    private void OnDisable()
    {
        _gammaManager.OnTrapDoorEnd -= TrapDoorEnd;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
        _gammaTrapDoorOver = GetComponent<GammaTrapDoorOver>();
    }

    private void TrapDoorEnd(GameObject trapDoor)
    {
        if (_gammaManager.IsTrapDoorOver)
        {
            _gammaManager.IsTrapDoorOver = false;
            MeshRenderer trapDoorMeshRenderer = trapDoor.GetComponent<MeshRenderer>();
            // Set the trap door's colour to its original colour
            trapDoorMeshRenderer.material.color = _gammaTrapDoorOver.OriginalColour;
        }
    }
}
