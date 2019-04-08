using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaPhotonAttach : MonoBehaviour
{
    [SerializeField] private GameObject _photon;

    private DeltaLevelManager _deltaManager;

    private void OnEnable() 
    {
        Setup();
        _deltaManager.OnPhotonAttach += PhotonAttach;
    }

    private void OnDisable() 
    {
        _deltaManager.OnPhotonAttach -= PhotonAttach;
    }

    private void Setup()
    {
        _deltaManager = GetComponent<DeltaLevelManager>();
    }

    private void PhotonAttach()
    {
        if (_deltaManager.PhotonsShot <= _deltaManager.MaxShootablePhotons)
        {
            // Instantiate photon 
            GameObject photon = Instantiate(_photon, _deltaManager.SpawnPosTrans.position, Quaternion.identity);
            // Set the photon's parent to the spawn pos' transform
            photon.transform.SetParent(_deltaManager.SpawnPosTrans);
            // Cache attached photon
            _deltaManager.CurrentPhoton = photon;
        }
    }
}
