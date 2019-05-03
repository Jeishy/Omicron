using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaPhotonShot : MonoBehaviour
{
    [Range(0.1f, 10.0f)] public float ShotForce;

    private Transform _ovrRemote;
    private DeltaLevelManager _deltaManager;

    private void OnEnable() 
    {
        Setup();
        _deltaManager.OnPhotonShoot += PhotonShoot;
    }

    private void OnDisable() 
    {
        _deltaManager.OnPhotonShoot -= PhotonShoot;
    }

    private void Setup()
    {
        _deltaManager = GetComponent<DeltaLevelManager>();
        _ovrRemote = GameObject.FindGameObjectWithTag("OculusRemote").transform;
    }

    private void PhotonShoot()
    {
        // Check if the number of photons shot are less than the number of photons that
        // can be shot and there is a photon attached
        if (_deltaManager.PhotonsShot < _deltaManager.MaxShootablePhotons && _deltaManager.IsPhotonAttached)
        {
            // Play photon shoot sound
            AudioManager.Instance.Play("PhotonShoot");
            _deltaManager.IsPhotonAttached = false;
            // Increment photons shot variable
            _deltaManager.PhotonsShot++;
            // Get forward direction of the remote
            Vector3 shotDir = _ovrRemote.forward;
            // Get the currently attached photon
            GameObject photon = _deltaManager.CurrentPhoton;
            // Get trail component
            TrailRenderer trail = photon.GetComponentInChildren<TrailRenderer>();
            // Renable trail when shot
            trail.enabled = true;
            // Dettach photon from its parent transform
            _deltaManager.SpawnPosTrans.DetachChildren();
            // Set photon's velocity to the forward direction of the remote * specified shot force
            Rigidbody photonRB = photon.GetComponent<Rigidbody>();
            photonRB.velocity = shotDir * ShotForce;
            // Cache the speed of the photon as its shot
            _deltaManager.MaxPhotonSpeed = photonRB.velocity.magnitude;
            // Set photon timer is shot bool to true, so the timer starts
            photon.GetComponent<DeltaPhotonTimer>().IsPhotonShot = true;
        }
    }
}
