using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonLevelManager : MonoBehaviour
{
    #region Delegates and Events
    // Delegate and events for epsilon level
    public delegate void PlayerEventManagerEpsilon();
    public event PlayerEventManagerEpsilon OnParticleShoot;
    public event PlayerEventManagerEpsilon OnPuzzleRestart;

    public delegate void ParticleEventManagerEpsilon(GameObject particle);
    public event ParticleEventManagerEpsilon OnParticleAttach;
    #endregion

    public float TimeTillParticlesDestroyed;                        // The time till particles in a nucleus are destroyed, upon puzzle completion


    [HideInInspector] public Transform OVRRemote;                   // The remote's transform
    [HideInInspector] public Transform ParticleSpawnTrans;          // The spawn point's transform
    [HideInInspector] public bool IsParticleAttached;               // Bool for if a photon is attached to the remote
    [HideInInspector] public GameObject CurrentAttachedParticle;    // The currently attached particle
    [HideInInspector] public int MaxQuarks;                         // The max quarks that can be used in a puzzle
    [HideInInspector] public int MaxBaryons;                        // The max baryons that can be used in a puzzle
    [HideInInspector] public int NumQuarksUsed;                     // The number of quarks used in a puzzle
    [HideInInspector] public int NumBaryonsUsed;                    // The number of baryons used in a puzzle

    private void Start()
    {
        // Get a reference to the remote's transform
        OVRRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        // Get reference to the transform of the spawn point for particles
        ParticleSpawnTrans = GameObject.FindGameObjectWithTag("BallSpawnPoint").transform;
        // Set is particle attached bool to false by default
        IsParticleAttached = false;
        // Set number of quarks and baryons used to 0 by default
        NumQuarksUsed = 0;
        NumBaryonsUsed = 0;
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
}
