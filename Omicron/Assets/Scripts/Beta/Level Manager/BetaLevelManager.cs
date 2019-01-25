using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaLevelManager : MonoBehaviour
{
    public delegate void PlayerEventManagerBeta();
    public event PlayerEventManagerBeta OnMagnetPlace;

    [HideInInspector] public bool IsMagnetPlaced;
    public void MagnetPlace()
    {
        if (OnMagnetPlace != null)
        {
            OnMagnetPlace();
        }
    }
}
