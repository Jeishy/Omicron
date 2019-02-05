using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaResetMagnetsInPuzzle : MonoBehaviour
{
    private BetaLevelManager betaManager;
    [SerializeField] private Transform spawnPoint;

    private void OnEnable()
    {
        Setup();
        betaManager.OnResetMagnets += Reset;
    }

    private void OnDisable()
    {
        betaManager.OnResetMagnets -= Reset;
    }

    private void Setup()
    {
        betaManager = GameObject.Find("BetaLevelManager").GetComponent<BetaLevelManager>();
    }

    private void Reset()
    {
        // Resets position of all magnets already in the puzzle (Not placeable magnets)
        transform.position = spawnPoint.position;           // Set there positions to their respective spawn point positions
        GetComponent<Rigidbody>().velocity = Vector3.zero;  // Set their velocities to 0 so that they dont move after being reset
    }
}
