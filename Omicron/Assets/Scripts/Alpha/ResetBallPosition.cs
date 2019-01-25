using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBallPosition: MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;

    [SerializeField] private GameObject ballSpawnPoint;
    [SerializeField] private GameObject ball;
    private void OnEnable()
    {
        Setup();
        alphaLevelManager.OnResetBallPosition += Reset;
    }

    private void OnDisable()
    {
        alphaLevelManager.OnResetBallPosition -= Reset;
    }

    private void Setup()
    {
        alphaLevelManager = GetComponent<AlphaLevelManager>();
    }

    private void Reset()
    {
        alphaLevelManager.IsGravityChanged = false;
        alphaLevelManager.IsBallShot = false;
        Vector3 ballSpawnPointPos = ballSpawnPoint.GetComponent<Transform>().position;
        ball.GetComponent<Transform>().position = ballSpawnPointPos;

        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Transform>().SetParent(ballSpawnPoint.transform);

        float ballSpeed = ball.GetComponent<Rigidbody>().velocity.magnitude;
        if (ballSpeed > 0)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
