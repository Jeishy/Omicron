using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaSetParticleDirections : MonoBehaviour
{
    [SerializeField] private Transform _positionDirectionTrans;

    private GammaLevelManager _gammaManager;
    private GammaParticle _gammaParticle;
    private Vector3 _initialPos;

    private void OnEnable()
    {
        Setup();
        _gammaManager.OnPuzzleStart += SetParticleDirections;
    }

    private void OnDisable()
    {
        _gammaManager.OnPuzzleStart -= SetParticleDirections;
    }

    private void Setup()
    {
        _gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
        _gammaParticle = GetComponent<GammaParticle>();
        _initialPos = transform.position;
    }

    private void SetParticleDirections()
    {
        Vector3 positionToMoveTowards = _positionDirectionTrans.position;
        Vector3 direction = Vector3.Normalize(positionToMoveTowards - _initialPos);
        _gammaParticle.ParticleDirection = direction;
    }
}
