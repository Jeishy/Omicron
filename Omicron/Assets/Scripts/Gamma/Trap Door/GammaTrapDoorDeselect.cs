using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaTrapDoorDeselect : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5.0f)] private float particleXShift;

    private GammaLevelManager _gammaManager;
    private GammaTrapDoorOver _gammaTrapDoorOver;
    private Collider trapDoorCol;

    private void OnEnable()
    {
        Setup();
        _gammaManager.OnTrapDoorDeselect += TrapDoorDeselect;
    }

    private void OnDisable()
    {
        _gammaManager.OnTrapDoorDeselect -= TrapDoorDeselect;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
        _gammaTrapDoorOver = GetComponent<GammaTrapDoorOver>();
    }

    private void TrapDoorDeselect(GameObject trapDoor)
    {
        // Set IsTrapDoorSelected to false
        _gammaManager.IsTrapDoorSelected = false;
        // Get the trap door's mesh renderer component
        MeshRenderer trapDoorMeshRenderer = trapDoor.GetComponent<MeshRenderer>();
        // Get the original colour stored in the _gammaTrapDoorOver class
        Color originalColour = _gammaTrapDoorOver.OriginalColour;
        // Set the trap door's colour to its original colour
        trapDoorMeshRenderer.material.color = originalColour;
        // Get the collider of the trap door
        trapDoorCol = trapDoor.GetComponent<Collider>();
        // Enable the collider of the trap door
        trapDoorCol.enabled = true;
        // Play trap door deselect sound
        AudioManager.Instance.Play("TrapDoorDeselect");
        List<GammaParticle> particles = _gammaManager.AllParticlesInPuzzle;
        foreach (GammaParticle particle in particles)
        {
            // Cycle through the list, and check if the particle is within the bounds of the collider on deselect
            Vector3 particlePos = particle.transform.position;
            Vector3 lastVelocity = particle.LastVelocity;
            if (trapDoorCol.bounds.Contains(particlePos))
            {
                // Set the particles position out of the collider 
                particle.transform.position = new Vector3(particlePos.x + particleXShift, particlePos.y, particlePos.z);
                // Set its velocity back to what it was
                particle.GetComponent<Rigidbody>().velocity = lastVelocity;
            }

        }
    }
}
