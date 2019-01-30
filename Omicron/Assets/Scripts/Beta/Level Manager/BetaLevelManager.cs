using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaLevelManager : MonoBehaviour
{
    public delegate void PlayerPlacementEventBeta(Vector3 remoteTarget);
    public event PlayerPlacementEventBeta OnMagnetPlaced;

    public delegate void PlayerEventManagerBeta();
    public event PlayerEventManagerBeta OnRestartPuzzle;
    public event PlayerEventManagerBeta OnMagnetAttach;
    public event PlayerEventManagerBeta OnReset;
    public event PlayerEventManagerBeta OnResetMagnets;

    [HideInInspector] public int MaxPlaceableMagnets;

    private void Start()
    {
        StartCoroutine("WaitToAttachMagnet");
    }

    private IEnumerator WaitToAttachMagnet()
    {
        yield return new WaitForSeconds(0.5f);
        MagnetAttach();
    }

    public void MagnetPlaced(Vector3 remoteTarget)
    {
        if (OnMagnetPlaced != null)
        {
            OnMagnetPlaced(remoteTarget);
        }
    }

    public void MagnetAttach()
    {
        if (OnMagnetAttach != null)
        {
            OnMagnetAttach();
        }
    }

    public void RestartPuzzle()
    {
        if (OnRestartPuzzle != null)
        {
            OnRestartPuzzle();
        }
    }

    public void Reset()
    {
        if (OnReset != null)
        {
            OnReset();
        }
    }

    public void ResetMagnets()
    {
        if (OnResetMagnets != null)
        {
            OnResetMagnets();
        }
    }
}
