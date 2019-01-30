using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaReset : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private BetaMagnetPlacement betaMagnetPlacement;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Setup();
        betaManager.OnReset += Reset;
    }

    private void OnDisable()
    {
        betaManager.OnReset -= Reset;
    }

    private void Setup()
    {
        betaManager = GetComponent<BetaLevelManager>();
        betaMagnetPlacement = GetComponent<BetaMagnetPlacement>();
    }

    private void Reset()
    {
        betaMagnetPlacement.ballsPlaced = 0;
    }
}
