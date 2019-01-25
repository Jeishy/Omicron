using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBallShoot : MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;
    [SerializeField] private Transform oculusGoTransform;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject ballSpawnPoint;
    [SerializeField][Range(0.1f, 20.0f)] private float ballShotSpeed;

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
    }

    private void Shoot()
    {
        if (alphaLevelManager.IsBallShot != true)
        {
            alphaLevelManager.IsBallShot = true;
            Vector3 direction = oculusGoTransform.forward;
            ball.GetComponent<Rigidbody>().useGravity = true;
            ballSpawnPoint.GetComponent<Transform>().DetachChildren();
            ball.GetComponent<Rigidbody>().velocity = direction * ballShotSpeed;
        }       
    }
}
