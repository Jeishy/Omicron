using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaLevelManager : MonoBehaviour
{
    #region Delegates and Events
    public delegate void PlayerEventManagerGamma();
    public event PlayerEventManagerGamma OnPuzzleStart;
    public event PlayerEventManagerGamma OnRestartPuzzle;
    public event PlayerEventManagerGamma OnPuzzleReset;

    public delegate void PlayerTrapDoorEvent(GameObject trapDoor);
    public event PlayerTrapDoorEvent OnTrapDoorSelect;
    public event PlayerTrapDoorEvent OnTrapDoorDeselect;
    public event PlayerTrapDoorEvent OnTrapDoorOver;
    public event PlayerTrapDoorEvent OnTrapDoorEnd;

    public delegate void ParticleEventManagerGamma(bool isHot);
    public event ParticleEventManagerGamma OnParticleStateChange;
    #endregion

    [HideInInspector] public int HotParticlesInPuzzle;                      // Number of hot particles in the currently active puzzle                          
    [HideInInspector] public int ColdParticlesInPuzzle;                     // Number of cold particles in the currently active puzzle 
    [HideInInspector] public bool IsTrapDoorOver;                           // Flag for if a trap door is being hovered over
    [HideInInspector] public bool IsTrapDoorSelected;                       // Flag for if a trap door has been selected
    [HideInInspector] public List<GammaParticle> ParticlesInPuzzle;         // A list of the particles in the currently active puzzle

    private void Start()
    {
        // Find all puzzles at the beginning of the level, and deactivate all but the first
        GameManager.Instance.FindAllPuzzles();
        // Call StartPuzzle method at beginning of level
        PuzzleStart();             
        // Set the bools to false at the begginning of the level
        IsTrapDoorOver = false;
        IsTrapDoorSelected = false;
    }


    // Function for running all methods subcribed to the OnPuzzleStart event
    public void PuzzleStart()
    {
        if (OnPuzzleStart != null)
        {
            OnPuzzleStart();
        }
    }

    // Function for running all methods subcribed to the OnRestartPuzzle event
    public void RestartPuzzle()
    {
        if (OnRestartPuzzle != null)
        {
            OnRestartPuzzle();
        }
    }

    // Function for running all methods subcribed to the OnFailPuzzle event
    public void PuzzleReset()
    {
        if (OnPuzzleReset != null)
        {
            OnPuzzleReset();
        }
    }

    // Function for running all methods subcribed to the OnTrapDoorSelect event
    public void TrapDoorSelect(GameObject trapDoor)
    {
        if (OnTrapDoorSelect != null)
        {
            OnTrapDoorSelect(trapDoor);
        }
    }

    // Function for running all methods subcribed to the OnTrapDoorDeselect event
    public void TrapDoorDeselect(GameObject trapDoor)
    {
        if (OnTrapDoorDeselect != null)
        {
            OnTrapDoorDeselect(trapDoor);
        }
    }

    // Function for running all methods subcribed to the OnTrapDoorOver event
    public void TrapDoorOver(GameObject trapDoor)
    {
        if (OnTrapDoorOver != null)
        {
            OnTrapDoorOver(trapDoor);
        }
    }

    // Function for running all methods subcribed to the OnTrapDoorEnd event
    public void TrapDoorEnd(GameObject trapDoor)
    {
        if (OnTrapDoorEnd != null)
        {
            OnTrapDoorEnd(trapDoor);
        }
    }

    // Function for running all methods subcribed to the OnParticleStateChange event
    public void ParticleStateChange(bool isHot)
    {
        if (OnParticleStateChange != null)
        {
            OnParticleStateChange(isHot);
        }
    }
}
