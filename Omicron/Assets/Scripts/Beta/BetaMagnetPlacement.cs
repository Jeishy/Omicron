using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaMagnetPlacement : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private GameManager gameManager;
    private BetaMagnetAttach magnetAttach;
    private Transform magnetSpawnPointTrans;
    private Rigidbody magnetRB;
    private BetaMagnetPooler bMagnetPooler;
    [HideInInspector] public int ballsPlaced;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        magnetAttach = GetComponent<BetaMagnetAttach>();
    }

    private void MagnetPlace(Vector3 targetPos)
    {
        magnetSpawnPointTrans = GameObject.Find("BallSpawnPoint").GetComponent<Transform>();
        // Check if all given magnets are placed
        if (ballsPlaced < betaManager.MaxPlaceableMagnets && magnetAttach.IsMagnetAttached == true)
        {
            magnetAttach.IsMagnetAttached = false;
            ballsPlaced++;
            magnetSpawnPointTrans.GetComponent<Transform>().DetachChildren();
            GameObject magnet = magnetAttach.currentMagnet;
            Transform magnetTrans = magnet.GetComponent<Transform>();
            Canvas magnetRadiusCanvas = magnet.GetComponentInChildren<Canvas>();

            magnetTrans.position = targetPos;
            magnetTrans.rotation = Quaternion.Euler(0, 0, 0);
            magnetRadiusCanvas.enabled = true;

            magnetTrans.SetParent(gameManager.FindActivePuzzle().GetComponent<Transform>());
            if (ballsPlaced != betaManager.MaxPlaceableMagnets)
                betaManager.MagnetAttach();
        }
    }
}
