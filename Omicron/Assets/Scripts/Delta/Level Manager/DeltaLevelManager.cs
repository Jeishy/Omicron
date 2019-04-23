using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaLevelManager : MonoBehaviour
{
    #region Delegates and Events
    // Delegate and events for delta level
    public delegate void PlayerEventManagerDelta();
    public event PlayerEventManagerDelta OnPhotonShoot;
    public event PlayerEventManagerDelta OnPhotonLost;
    public event PlayerEventManagerDelta OnPhotonAttach;
    public event PlayerEventManagerDelta OnPuzzleReset;
    #endregion

    [HideInInspector] public int PhotonsShot;
    [HideInInspector] public int PhotonsInGoal;
    [HideInInspector] public int MaxShootablePhotons;
    [HideInInspector] public GameObject CurrentPhoton;
    [HideInInspector] public Transform SpawnPosTrans;
    [HideInInspector] public float MaxPhotonSpeed;
    [HideInInspector] public bool IsPhotonAttached;

    private void Start()
    {
        SpawnPosTrans = GameObject.Find("BallSpawnPoint").transform;
        // Set photons shot to 0 at the beginning of the level
        PhotonsShot = 0;
        // Set max shootable photons to 1 by default
        MaxShootablePhotons = 1;
        // Find all puzzles in the level
        GameManager.Instance.FindAllPuzzles();
        // Attach a photon to the front of the oculus remote
        PhotonAttach();
    }

    // Function for running all functions subscribed to the OnPhotonShoot event
    public void PhotonShoot()
    {
        if (OnPhotonShoot != null)
        {
            OnPhotonShoot();
        }
    }

    // Function for running all functions subscribed to the OnPhotonLost event
    public void PhotonLost()
    {
        if (OnPhotonLost != null)
        {
            OnPhotonLost();
        }
    }

    // Function for running all functions subscribed to the OnPuzzleReset event
    public void PuzzleReset()
    {
        if (OnPuzzleReset != null)
        {
            OnPuzzleReset();
        }
    }

    // Function for running all functions subscribed to the OnPhotonAttach event
    public void PhotonAttach()
    {
        if (OnPhotonAttach != null)
        {
            OnPhotonAttach();
        }
    }
}
