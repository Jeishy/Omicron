using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonLevelManager : MonoBehaviour
{
    #region Delegates and Events
    // Delegate and events for delta level
    public delegate void PlayerEventManagerEpsilon();
    public event PlayerEventManagerEpsilon OnParticleAttach;
    public event PlayerEventManagerEpsilon OnParticlePlace;
    public event PlayerEventManagerEpsilon OnPuzzleRestart;
    
    #endregion

    private void Start()
    {
        // Find all puzzles in the level
        GameManager.Instance.FindAllPuzzles();
    }

    // Function for running all functions subscribed to the OnParticleAttach event
    public void ParticleAttach()
    {
        if (OnParticleAttach != null)
        {
            OnParticleAttach();
        }
    }

    // Function for running all functions subscribed to the OnParticlePlace event
    public void ParticlePlace()
    {
        if (OnParticlePlace != null)
        {
            OnParticlePlace();
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
}
