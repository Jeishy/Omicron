using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaTrapDoorSelect : MonoBehaviour
{
    [SerializeField][Range(0.0f, 1.0f)] private float alphaValue;

    private GammaLevelManager _gammaManager;
    private GammaTrapDoorOver _gammaTrapDoorOver;

    private void OnEnable()
    {
        Setup();
        _gammaManager.OnTrapDoorSelect += TrapDoorSelect;
    }

    private void OnDisable()
    {
        _gammaManager.OnTrapDoorSelect -= TrapDoorSelect;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
        _gammaTrapDoorOver = GetComponent<GammaTrapDoorOver>();
    }

    private void TrapDoorSelect(GameObject trapDoor)
    {
        _gammaManager.IsTrapDoorSelected = true;
        MeshRenderer trapDoorMeshRenderer = trapDoor.GetComponent<MeshRenderer>();
        Color originalColour = _gammaTrapDoorOver.OriginalColour;
        trapDoorMeshRenderer.material.color = new Color(originalColour.r, originalColour.g, originalColour.b, alphaValue);
        trapDoor.GetComponent<Collider>().enabled = false;
    }
}
