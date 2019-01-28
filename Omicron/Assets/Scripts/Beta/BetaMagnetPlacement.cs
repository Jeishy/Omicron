using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaMagnetPlacement : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private BetaMagnetAttach magnetAttach;
    private Transform magnetSpawnPointTrans;
    private Rigidbody magnetRB;
    private BetaMagnetPooler bMagnetPooler;
    private int ballsPlaced;
    [SerializeField] private Text debugText;

    private void OnEnable()
    {
        Setup();
        betaManager.OnMagnetPlaced += MagnetPlace;
    }

    private void OnDisable()
    {
        betaManager.OnMagnetPlaced -= MagnetPlace;
    }

    private void Setup()
    {
        ballsPlaced = 0;
        betaManager = GetComponent<BetaLevelManager>();
        magnetAttach = GetComponent<BetaMagnetAttach>();
        magnetSpawnPointTrans = GameObject.Find("BallSpawnPoint").GetComponent<Transform>();
    }

    private void MagnetPlace(Vector3 targetPos)
    {
        // Check if all given magnets are placed
        if (ballsPlaced < betaManager.MaxPlaceableMagnets)
        {
            ballsPlaced++;
            magnetSpawnPointTrans.GetComponent<Transform>().DetachChildren();
            GameObject magnet = magnetAttach.currentMagnet;
            magnet.GetComponent<Transform>().position = targetPos;
            magnet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            if (ballsPlaced != betaManager.MaxPlaceableMagnets)
                betaManager.MagnetAttach();
        }
    }
}
