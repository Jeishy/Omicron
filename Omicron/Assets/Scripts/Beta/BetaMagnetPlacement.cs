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
    [HideInInspector] public int ballsPlaced;   // Variable that holds number of ballsPlaced
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
        ballsPlaced = 0;    // Sets balls placed to 0 at beginning of level
        betaManager = GetComponent<BetaLevelManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        magnetAttach = GetComponent<BetaMagnetAttach>();
        magnetSpawnPointTrans = PlayerControllerReferences.Instance.BallSpawnPoint;
    }

    private void MagnetPlace(Vector3 targetPos)
    {
        // Check if all given magnets are not placed
        // and there is an attached magnet
        if (ballsPlaced < betaManager.MaxPlaceableMagnets && magnetAttach.IsMagnetAttached == true)
        {
            magnetAttach.IsMagnetAttached = false;                                // A magnet has been placed, set IsMagnetAttached to false
            ballsPlaced++;                                                        // Increment number of balls placed by one
            magnetSpawnPointTrans.GetComponent<Transform>().DetachChildren();     // Detach magnet from spawn point's transform
            GameObject magnet = magnetAttach.currentMagnet;                      
            Transform magnetTrans = magnet.GetComponent<Transform>();
            Canvas magnetRadiusCanvas = magnet.GetComponentInChildren<Canvas>();  // Turns on canvas used for seeing magnet's radius

            magnetTrans.position = targetPos;                                     // Set parsed in target position to position of magnet to be placed
            magnetTrans.rotation = Quaternion.Euler(0, 0, 0);                     // Set rotation of magnet to 0 on all axes
            magnetRadiusCanvas.enabled = true;                                    // Turns on canvas for seeing magnets radius

            magnetTrans.SetParent(gameManager.FindActivePuzzle().GetComponent<Transform>());

            // If there are more balls to be placed
            // attach another magnet to the end of the remote
            if (ballsPlaced != betaManager.MaxPlaceableMagnets)
                betaManager.MagnetAttach();
        }
    }
}
