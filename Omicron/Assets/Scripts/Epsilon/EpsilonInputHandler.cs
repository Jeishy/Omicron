using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonInputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private EpsilonLevelManager _epsilonManager;
    private Transform _ovrRemoteTrans;

    // Start is called before the first frame update
    private void Start()
    {
        _ovrRemoteTrans = GameObject.FindGameObjectWithTag("OculusRemote").transform;
        _epsilonManager = GetComponent<EpsilonLevelManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            PCInput();
        else
            VRInput();
    }

    private void VRInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            RaycastHit hit;
            // Get remote's position and forward direction
            Vector3 remoteDirection = _ovrRemoteTrans.forward;
            Vector3 remotePos = _ovrRemoteTrans.position;

            if (_epsilonManager.IsParticleAttached)
            {
                Debug.Log("Firing particle");
                // If a particle is already attached, trigger the OnParticleShoot event
                _epsilonManager.ParticleShoot();   
            }
            // Check if remote is aiming at a particle
            else if (Physics.Raycast(remotePos, remoteDirection, out hit, Mathf.Infinity, _layerMask))
            {
                // Get the collider component of the hit gameobject for a tag comparison check
                Collider particleCol = hit.collider;
                // Get the GO component of the particle
                GameObject particleGO = hit.collider.gameObject;

                // Check if the particle is a shelf particle
                if (particleCol.CompareTag("ShelfQuark"))
                {
                    // Attach the particle to the remote
                    _epsilonManager.ParticleAttach(particleGO);
                }
            }

        }
    }

    private void PCInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Uses raycast to see if BetaBox placeable zone is being targetted when mouse button is clicked
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_epsilonManager.IsParticleAttached)
            {
                Debug.Log("Firing particle");
                // If a particle is already attached, trigger the OnParticleShoot event
                _epsilonManager.ParticleShoot();   
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                // Get the collider component of the hit gameobject for a tag comparison check
                Collider particleCol = hit.collider;
                // Get the GO component of the particle
                GameObject particleGO = hit.collider.gameObject;
                if (particleCol.CompareTag("ShelfQuark"))
                {
                    _epsilonManager.ParticleAttach(particleGO);
                }
            }
        }
    }
}
