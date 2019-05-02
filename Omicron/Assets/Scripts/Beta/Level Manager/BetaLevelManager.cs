using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaLevelManager : MonoBehaviour
{
    #region Delegates and Events
    // Delegate and event for placing the magnets
    public delegate void PlayerPlacementEventBeta(Vector3 remoteTarget);
    public event PlayerPlacementEventBeta OnMagnetPlaced;

    // Delegate and events for beta level
    public delegate void PlayerEventManagerBeta();
    public event PlayerEventManagerBeta OnMagnetAttach;
    public event PlayerEventManagerBeta OnReset;
    public event PlayerEventManagerBeta OnResetMagnets;
    #endregion

    // Max placeable magnets; this is overwritten by each puzzle
    [HideInInspector] public int MaxPlaceableMagnets;

    private void Start()
    {
        // Attach a new magnet at the start of the level
        MagnetAttach();
        // Find all puzzles at the beginning of the level, and deactivate all but the first
        GameManager.Instance.FindAllPuzzles();
    }

    // Function for running all methods subscribed to OnMagnetPlaced event
    public void MagnetPlaced(Vector3 remoteTarget)
    {
        if (OnMagnetPlaced != null)
        {
            OnMagnetPlaced(remoteTarget);
        }
    }

    // Function for running all methods subscribed to OnMagnetAttach event
    public void MagnetAttach()
    {
        if (OnMagnetAttach != null)
        {
            OnMagnetAttach();
        }
    }

    // Function for running all methods subscribed to OnReset event
    public void Reset()
    {
        if (OnReset != null)
        {
            OnReset();
        }
    }

    // Function for running all methods subscribed to OnResetMagnets event
    public void ResetMagnets()
    {
        if (OnResetMagnets != null)
        {
            OnResetMagnets();
        }
    }
}
