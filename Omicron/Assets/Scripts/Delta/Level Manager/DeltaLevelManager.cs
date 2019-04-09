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
    public event PlayerEventManagerDelta OnPhotonReset;
    public event PlayerEventManagerDelta OnPhotonAttach;
    #endregion

    [HideInInspector] public int PhotonsShot;
    [HideInInspector] public int PhotonsInGoal;
    [HideInInspector] public int MaxShootablePhotons;
    [HideInInspector] public GameObject CurrentPhoton;
    [HideInInspector] public Transform SpawnPosTrans;

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

    public void PhotonShoot()
    {
        if (OnPhotonShoot != null)
        {
            OnPhotonShoot();
        }
    }

    public void PhotonLost()
    {
        if (OnPhotonLost != null)
        {
            OnPhotonLost();
        }
    }

    public void PhotonReset()
    {
        if (OnPhotonReset != null)
        {
            OnPhotonReset();
        }
    }

    public void PhotonAttach()
    {
        if (OnPhotonAttach != null)
        {
            OnPhotonAttach();
        }
    }
}
