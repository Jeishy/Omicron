using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBallDropped : MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;
    [SerializeField] private Transform ball;
    [SerializeField] private float maxBallDistance;
    [SerializeField] private Transform ovrCameraRigTrans;

    private void OnEnable()
    {
        Setup();
        alphaLevelManager.OnBallDropped += Dropped;
    }

    private void OnDisable()
    {
        alphaLevelManager.OnBallDropped -= Dropped;
    }

    private void Setup()
    {
        alphaLevelManager = GetComponent<AlphaLevelManager>();
    }

    private void Dropped()
    {
        // Run some shader code and wait till finished
        // Spawn ball at position of remote
        Vector3 ballPos = ball.position;
        Vector3 playerPos = ovrCameraRigTrans.position;
        float distanceFromPlayerToBall = Vector3.Distance(playerPos, ballPos);

        if (distanceFromPlayerToBall > maxBallDistance)
        {
            alphaLevelManager.ResetBallPosition();
        }
    }
}
