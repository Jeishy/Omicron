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

    [HideInInspector] public bool IsPhotonShot;
    [HideInInspector] public int MaxPlaceablePhotons;

    private void Start()
    {
        IsPhotonShot = false;
        GameManager.Instance.FindAllPuzzles();
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
