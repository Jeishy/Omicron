using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBallPosition: MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;

    // Reference to the spawn point of the ball, on the controller
    private Transform ballSpawnPoint;
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
        // Sets cached reference of the balls spawn point in the PlayerControllerRefereneces class
        ballSpawnPoint = GameObject.FindGameObjectWithTag("BallSpawnPoint").transform;
    }

    private void Reset()
    {
        // Play puzzle failed sound
        AudioManager.Instance.Play("PuzzleFail");
        alphaLevelManager.IsGravityChanged = false;                          // Sets IsGravityChanged back to false, so that standard gravity can be used
        alphaLevelManager.IsBallShot = false;                                // Sets IsBallShot to false, so that the ball can be shot again
        Vector3 ballSpawnPointPos = ballSpawnPoint.position;                 // Cache the ball spawn point's position

        ball.transform.position = ballSpawnPointPos;                         // Set balls position to cached spawn point position

        ball.GetComponent<Rigidbody>().useGravity = false;                   // Set gravity to false, so that the ball does not continue to fall
        ball.GetComponent<Transform>().SetParent(ballSpawnPoint.transform);  // Set balls parent transform to ballSpawnPoint

        float ballSpeed = ball.GetComponent<Rigidbody>().velocity.magnitude;
        // If the calculated balls speed is greater than 0, set it to 0
        // This ensures the ball doesn't continue to fall once attached to the front of the oculus go remote
        if (ballSpeed > 0)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
