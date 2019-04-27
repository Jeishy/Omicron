using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaLevelManager : MonoBehaviour
{
    #region Delegates and Events
    public delegate void PlayerEventManagerGamma();
    public event PlayerEventManagerGamma OnPuzzleStart;
    public event PlayerEventManagerGamma OnPuzzleRestart;
    public event PlayerEventManagerGamma OnPuzzleComplete;

    public delegate void PlayerTrapDoorEvent(GameObject trapDoor);
    public event PlayerTrapDoorEvent OnTrapDoorSelect;
    public event PlayerTrapDoorEvent OnTrapDoorDeselect;
    public event PlayerTrapDoorEvent OnTrapDoorOver;
    public event PlayerTrapDoorEvent OnTrapDoorEnd;

    public delegate void ParticleEventManagerGamma(GammaParticle gammaParticle);
    public event ParticleEventManagerGamma OnParticleStateChange;
    #endregion

    [HideInInspector] public List<GammaParticle> AllParticlesInPuzzle;      // All the particles in the current puzzle
    [HideInInspector] public List<GammaParticle> HotParticlesInPuzzle;      // List of hot particles in the current puzzle                          
    [HideInInspector] public List<GammaParticle> ColdParticlesInPuzzle;     // List of cold particles in the current puzzle 
    [HideInInspector] public bool IsTrapDoorOver;                           // Flag for if a trap door is being hovered over
    [HideInInspector] public bool IsTrapDoorSelected;                       // Flag for if a trap door has been selected
    [HideInInspector] public bool IsPuzzleCompleted;

    private void Start()
    {
        // Find all puzzles at the beginning of the level, and deactivate all but the first
        GameManager.Instance.FindAllPuzzles();
        // Call StartPuzzle method at beginning of level
        PuzzleStart();             
        // Set the bools to false at the beginning of the level
        IsTrapDoorOver = false;
        IsTrapDoorSelected = false;
        IsPuzzleCompleted = false;
    }

    public void SetHotParticlesInPuzzle()
    {     
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        GammaParticle[] particles = activePuzzle.GetComponentsInChildren<GammaParticle>();

        foreach (GammaParticle particle in particles)
        {
            if (particle.IsHot)
                HotParticlesInPuzzle.Add(particle);
        }
    }

    public void SetColdParticlesInPuzzle()
    {
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        GammaParticle[] particles = activePuzzle.GetComponentsInChildren<GammaParticle>();

        foreach (GammaParticle particle in particles)
        {
            if (!particle.IsHot)
                ColdParticlesInPuzzle.Add(particle);
        }
    }

    // Function for running all methods subscribed to the OnPuzzleStart event
    public void PuzzleStart()
    {
        if (OnPuzzleStart != null)
        {
            OnPuzzleStart();
        }
    }

    // Function for running all methods subscribed to the OnRestartPuzzle event
    public void PuzzleRestart()
    {
        if (OnPuzzleRestart != null)
        {
            OnPuzzleRestart();
        }
    }

    // Function for running all methods subscribed to the OnPuzzleComplete event
    public void PuzzleComplete()
    {
        if (OnPuzzleComplete != null)
        {
            OnPuzzleComplete();
        }
    }

    // Function for running all methods subscribed to the OnTrapDoorSelect event
    public void TrapDoorSelect(GameObject trapDoor)
    {
        if (OnTrapDoorSelect != null)
        {
            OnTrapDoorSelect(trapDoor);
        }
    }

    // Function for running all methods subscribed to the OnTrapDoorDeselect event
    public void TrapDoorDeselect(GameObject trapDoor)
    {
        if (OnTrapDoorDeselect != null)
        {
            OnTrapDoorDeselect(trapDoor);
        }
    }

    // Function for running all methods subscribed to the OnTrapDoorOver event
    public void TrapDoorOver(GameObject trapDoor)
    {
        if (OnTrapDoorOver != null)
        {
            OnTrapDoorOver(trapDoor);
        }
    }

    // Function for running all methods subscribed to the OnTrapDoorEnd event
    public void TrapDoorEnd(GameObject trapDoor)
    {
        if (OnTrapDoorEnd != null)
        {
            OnTrapDoorEnd(trapDoor);
        }
    }

    // Function for running all methods subscribed to the OnParticleStateChange event
    public void ParticleStateChange(GammaParticle gammaParticle)
    {
        if (OnParticleStateChange != null)
        {
            OnParticleStateChange(gammaParticle);
        }
    }
}
