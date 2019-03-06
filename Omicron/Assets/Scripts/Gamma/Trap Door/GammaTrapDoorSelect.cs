using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaTrapDoorSelect : MonoBehaviour
{
    [SerializeField][Range(0.0f, 1.0f)] private float alphaValue;

    private GammaLevelManager gammaManager;
    private GammaTrapDoorOver gammaTrapDoorOver;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnTrapDoorSelect += TrapDoorSelect;
    }

    private void OnDisable()
    {
        gammaManager.OnTrapDoorSelect -= TrapDoorSelect;
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
        gammaTrapDoorOver = GetComponent<GammaTrapDoorOver>();
    }

    private void TrapDoorSelect(GameObject trapDoor)
    {
        gammaManager.IsTrapDoorSelected = true;
        MeshRenderer trapDoorMeshRenderer = trapDoor.GetComponent<MeshRenderer>();
        Color originalColour = gammaTrapDoorOver.OriginalColour;
        trapDoorMeshRenderer.material.color = new Color(originalColour.r, originalColour.g, originalColour.b, alphaValue);
        trapDoor.GetComponent<Collider>().enabled = false;
    }
}
