using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaInputHandler : MonoBehaviour
{
    private DeltaLevelManager _deltaManager;
    // Start is called before the first frame update
    private void Start()
    {
        _deltaManager = GetComponent<DeltaLevelManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        // If trigger is squeezed, shoot a photon and attach a new one
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            Shoot();
            // Only attach a new photon if the number of photons shot are above 0
            if (_deltaManager.PhotonsShot > 0 && _deltaManager.PhotonsShot != _deltaManager.MaxShootablePhotons)
            {
                StopAllCoroutines();
                StartCoroutine(Attach());
            }
        }

        // Reset puzzle if track pad pressed
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote))
        {
            _deltaManager.PuzzleReset();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _deltaManager.PuzzleReset();
        }
    }

    private void Shoot()
    {
        // Shoot a photon
        _deltaManager.PhotonShoot();
        // Play photon shot particle effect
    }

    private IEnumerator Attach()
    {
        // Wait x seconds before attaching a new photon
        yield return new WaitForSeconds(1f);
        // Play photon attach particle effect
        _deltaManager.PhotonAttach();
    }
}
