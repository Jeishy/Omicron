using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonLevelManager : MonoBehaviour
{
    #region Delegates and Events
    // Delegate and events for epsilon level
    public delegate void PlayerEventManagerEpsilon();
    public event PlayerEventManagerEpsilon OnParticleShoot;
    public event PlayerEventManagerEpsilon OnPuzzleComplete;
    public event PlayerEventManagerEpsilon OnPuzzleRestart;

    public delegate void ParticleEventManagerEpsilon(GameObject particle);
    public event ParticleEventManagerEpsilon OnParticleAttach;

    public delegate void ParticleCreateEventEpsilon(GameObject particleOne, GameObject particleTwo);
    public event ParticleCreateEventEpsilon OnParticleCreate;

    #endregion

    [HideInInspector] public bool IsParticleAttached;               // Bool for if a photon is attached to the remote
    [HideInInspector] public GameObject CurrentAttachedParticle;     // The currently attached particle

    private void Start()
    {
        // Set is particle attached bool to false by default
        IsParticleAttached = false;
        // Find all puzzles in the level
        GameManager.Instance.FindAllPuzzles();
    }

    // Function for running all functions subscribed to the OnParticleShoot event
    public void ParticleShoot()
    {
        if (OnParticleShoot != null)
        {
            OnParticleShoot();
        }
    }

    // Function for running all the functions subscribed to the OnPuzzleComplete event
    public void PuzzleComplete()
    {
        if (OnPuzzleComplete != null)
        {
            OnPuzzleComplete();
        }
    }

    // Function for running all functions subscribed to the OnPuzzleRestart event
    public void PuzzleRestart()
    {
        if (OnPuzzleRestart != null)
        {
            OnPuzzleRestart();
        }
    }

    // Function for running all functions subscribed to the OnParticleSelect event
    public void ParticleAttach(GameObject particle)
    {
        if (OnParticleAttach != null)
        {
            OnParticleAttach(particle);
        }
    }

    // Function for running all functions subscribed to the OnParticleCreate event
    public void ParticleCreate(GameObject particleOne, GameObject particleTwo)
    {
        if (OnParticleCreate != null)
        {
            OnParticleCreate(particleOne, particleTwo);
        }
    }
}
