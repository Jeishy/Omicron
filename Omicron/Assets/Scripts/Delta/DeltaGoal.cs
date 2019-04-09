using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaGoal : MonoBehaviour
{
    [SerializeField] private float _emissionIntenisty;
    private DeltaLevelManager _deltaManager;
    private MeshRenderer _meshRenderer;
    private bool _hasPhotonHit;

    private void Start() 
    {
        _deltaManager = GameObject.Find("DeltaLevelManager").GetComponent<DeltaLevelManager>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _hasPhotonHit = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Photon") && !_hasPhotonHit)
        {
            // Set has hit bool to true, so no other photon can hit it
            _hasPhotonHit = true;
            // Increase goal material's emission intensity
            Color emissionColour = _meshRenderer.material.GetColor("_EmissionColor");
            _meshRenderer.material.SetColor("_EmissionColor", emissionColour * _emissionIntenisty);
            _deltaManager.PhotonsInGoal++;
            // Play particle effect here
            //
            Destroy(other.gameObject);
            // Go to the next puzzle if the number of shootable photons equal the number of photons
            // that have reached the goal
            if (_deltaManager.PhotonsInGoal == _deltaManager.MaxShootablePhotons)
            {
                _hasPhotonHit = false;
                // Set the material's colour back to normal
                _meshRenderer.material.SetColor("_EmissionColor", emissionColour);
                // Go to the next puzzle
                GameManager.Instance.NextPuzzle();
                // Set photons shot back to 0
                _deltaManager.PhotonsShot = 0;
            }
        }   
    }
}
