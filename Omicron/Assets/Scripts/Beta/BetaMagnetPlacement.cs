using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaMagnetPlacement : MonoBehaviour
{
    private BetaLevelManager betaManager;

    private void OnEnable()
    {
        Setup();
        betaManager.OnMagnetPlace += MagnetPlace;
    }

    private void OnDisable()
    {
        betaManager.OnMagnetPlace -= MagnetPlace;
    }

    private void Setup()
    {
        betaManager = GetComponent<BetaLevelManager>();
    }

    private void MagnetPlace()
    {
        if (betaManager.IsMagnetPlaced != true)
        {
            // Do magnet placing stuff
        }
    }
}
