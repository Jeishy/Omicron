using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaTrapDoorOver : MonoBehaviour
{
    [HideInInspector] public Color OriginalColour;

    [SerializeField] [Range(0.0f, 0.99f)] private float alphaValue;

    private GammaLevelManager gammaManager;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnTrapDoorOver += TrapDoorOver;
    }

    private void OnDisable()
    {
        gammaManager.OnTrapDoorOver -= TrapDoorOver;
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
    }

    private void TrapDoorOver(GameObject trapDoor)
    {
        if (!gammaManager.IsTrapDoorOver)
        {
            gammaManager.IsTrapDoorOver = true;
            MeshRenderer trapDoorMeshRenderer = trapDoor.GetComponent<MeshRenderer>();
            // Get the OriginalColour of the trap door
            OriginalColour = trapDoorMeshRenderer.material.color;
            // Reduce the opacity of the trap door slightly
            trapDoorMeshRenderer.material.color = new Color(OriginalColour.r, OriginalColour.g, OriginalColour.b, alphaValue);
        }
    }
}
