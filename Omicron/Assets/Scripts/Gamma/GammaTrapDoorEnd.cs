using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaTrapDoorEnd : MonoBehaviour
{
    private GammaLevelManager gammaManager;
    private GammaTrapDoorOver gammaTrapDoorOver;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnTrapDoorEnd += TrapDoorEnd;
    }

    private void OnDisable()
    {
        gammaManager.OnTrapDoorEnd -= TrapDoorEnd;
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
        gammaTrapDoorOver = GetComponent<GammaTrapDoorOver>();
    }

    private void TrapDoorEnd(GameObject trapDoor)
    {
        if (gammaManager.IsTrapDoorOver)
        {
            Debug.Log("Trap door end!!!!!!!!!!!!!!!!");
            gammaManager.IsTrapDoorOver = false;
            MeshRenderer trapDoorMeshRenderer = trapDoor.GetComponent<MeshRenderer>();
            // Set the trap door's colour to its original colour
            trapDoorMeshRenderer.material.color = gammaTrapDoorOver.OriginalColour;
        }
    }
}
