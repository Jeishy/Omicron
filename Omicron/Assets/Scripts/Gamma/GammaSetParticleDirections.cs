using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaSetParticleDirections : MonoBehaviour
{
    [SerializeField] private Transform positionDirecitonTrans;


    private GammaLevelManager gammaManager;
    private GammaParticle gammaParticle;
    private Vector3 initialPos;
    private float randomX;
    private float randomY;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnPuzzleStart += SetParticleDirections;
    }

    private void OnDisable()
    {
        gammaManager.OnPuzzleStart -= SetParticleDirections;
    }

    private void Setup()
    {
        gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
        gammaParticle = GetComponent<GammaParticle>();
        initialPos = transform.position;
    }

    private void SetParticleDirections()
    {
        Vector3 positionToMoveTowards = positionDirecitonTrans.position;
        Vector3 direction = Vector3.Normalize(positionToMoveTowards - initialPos);
        Debug.Log(gameObject.name + "'s direction vector is: " + direction);
        gammaParticle.ParticleDirection = direction;
    }
}
