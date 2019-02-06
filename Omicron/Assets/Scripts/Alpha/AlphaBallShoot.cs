using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBallShoot : MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;
    private Transform oculusGoTransform;
    [SerializeField] private GameObject ball;
    private Transform ballSpawnPoint;
    [SerializeField][Range(0.1f, 20.0f)] private float ballShotSpeed;   // Speed at which the fall is shot

    private void OnEnable()
    {
        Setup();
        alphaLevelManager.OnBallShot += Shoot;
    }

    private void OnDisable()
    {
        alphaLevelManager.OnBallShot -= Shoot;
    }

    private void Setup()
    {
        alphaLevelManager = GetComponent<AlphaLevelManager>();
        oculusGoTransform = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        ballSpawnPoint = GameObject.FindGameObjectWithTag("BallSpawnPoint").transform;
    }

    private void Shoot()
    {
        // If ball hasnt the been shot,
        // flag it as having been shot,
        // turn back on balls gravity
        // Detach is from the OculusGoRemoteModel's transform
        // and set its velocity to the calculated direcition and specified speed
        if (alphaLevelManager.IsBallShot != true)
        {
            alphaLevelManager.IsBallShot = true;
            Vector3 direction = oculusGoTransform.forward;
            ball.GetComponent<Rigidbody>().useGravity = true;
            ballSpawnPoint.DetachChildren();
            ball.GetComponent<Rigidbody>().velocity = direction * ballShotSpeed;
        }       
    }
}
