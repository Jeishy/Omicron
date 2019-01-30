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
        Debug.Log("Resetting south magnet");
        transform.position = spawnPoint.position;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
