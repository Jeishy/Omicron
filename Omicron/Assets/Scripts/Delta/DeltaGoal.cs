﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaGoal : MonoBehaviour
{
    [HideInInspector] public Color OriginalColour;
    [HideInInspector] public bool HasPhotonHit;

    [SerializeField] private float _emissionIntenisty;
    [SerializeField][Range(0.01f, 3.0f)] private float _nextPuzzleWaitTime;

    private DeltaLevelManager _deltaManager;
    private MeshRenderer _meshRenderer;

    private void Start() 
    {
        _deltaManager = GameObject.Find("DeltaLevelManager").GetComponent<DeltaLevelManager>();
        _meshRenderer = GetComponent<MeshRenderer>();
        HasPhotonHit = false;
        // Get the original emission colour
        OriginalColour = _meshRenderer.material.GetColor("_EmissionColor");
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Photon") && !HasPhotonHit)
        {
            // Set has hit bool to true, so no other photon can hit it
            HasPhotonHit = true;
            // Increase goal material's emission intensity
            _meshRenderer.material.SetColor("_EmissionColor", OriginalColour * _emissionIntenisty);
            _deltaManager.PhotonsInGoal++;
            // Play particle effect here
            //
            Destroy(other.gameObject);
            // Go to the next puzzle if the number of shootable photons equal the number of photons
            // that have reached the goal
            if (_deltaManager.PhotonsInGoal == _deltaManager.MaxShootablePhotons)
            {
                HasPhotonHit = false;
                // Set the material's colour back to normal
                _meshRenderer.material.SetColor("_EmissionColor", OriginalColour);
                StartCoroutine(NextPuzzleDelay());
            }
        }   
    }

    private IEnumerator NextPuzzleDelay()
    {
        // Wait a few sonds before going to the next puzzle
        yield return new WaitForSeconds(_nextPuzzleWaitTime);
        // Set photons shot back to 0
        _deltaManager.PhotonsShot = 0;
        // Attach a new photon
        _deltaManager.PhotonAttach();
        // Go to the next puzzle
        GameManager.Instance.NextPuzzle();
    }
}