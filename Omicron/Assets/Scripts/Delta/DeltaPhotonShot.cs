using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaPhotonShot : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 10.0f)] private float _shotForce;

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
        // Get forward direction of the remote
        Vector3 shotDir = _ovrRemote.forward;
        // Get the currently attached photon
        GameObject photon = _deltaManager.CurrentPhoton;
        // Dettach photon from its parent transform
        _deltaManager.SpawnPosTrans.DetachChildren();
        // Set photon's velocity to the forward direction of the remote * specified shot force
        Rigidbody photonRB = photon.GetComponent<Rigidbody>();
        photonRB.velocity = shotDir * _shotForce;
    }
}
