using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonNucleus : MonoBehaviour
{
    [SerializeField] private Baryon _desiredBaryon;
    [SerializeField] private Animator _nucluesBaryonAnimator;

    private List<EpsilonQuark> _epsilonQuarksInNucleus = new List<EpsilonQuark>();
    private List<EpsilonBaryon> _epsilonBaryonsInNucleus = new List<EpsilonBaryon>();
    private int _chargeInNucleus;
    private EpsilonLevelManager _epsilonManager;

    private void Start() 
    {
        // Set charge in nucleus to be neutral (to 0)
        _chargeInNucleus = 0;
        // Get reference to the epsilon level manager
        _epsilonManager = GameObject.Find("EpsilonLevelManager").GetComponent<EpsilonLevelManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check if a particle has entered the nucleus
        if (col.gameObject.layer == 17)
        {
            // Get reference to the particles epsilon particle component
            EpsilonParticle _epsilonParticle = col.gameObject.GetComponent<EpsilonParticle>();

            if (col.CompareTag("ShelfQuark"))
            {
                // Add the quark to the list of quarks in the nucleus
                EpsilonQuark epsilonQuark = col.gameObject.GetComponent<EpsilonQuark>();
                _epsilonQuarksInNucleus.Add(epsilonQuark);
                CheckQuarksInNucleus();
            }
            else if (col.CompareTag("Baryon"))
            {
                // Add the quark to the list of quarks in the nucleus
                EpsilonBaryon epsilonBaryon = col.gameObject.GetComponent<EpsilonBaryon>();
                _epsilonBaryonsInNucleus.Add(epsilonBaryon);
            }
            
            // Set Nucleus centre trans to transform of nucleus
            _epsilonParticle.NucleusCentreTrans = transform;
            // Set CanOrbit bool to true
            _epsilonParticle.CanOrbit = true;
        }
    }

    private void CheckQuarksInNucleus()
    {
        _chargeInNucleus = 0;
        foreach (EpsilonQuark quark in _epsilonQuarksInNucleus)
        {
            // Get the sum charge of the nucleus
            _chargeInNucleus += quark.Charge;
        }

        // Get the charge of the baryon
        int bayronCharge = GetBaryonCharge();

        if (_chargeInNucleus == bayronCharge)
        {
            // Destroy all quarks in nucleus
            DestroyParticlesInNucleus(_epsilonQuarksInNucleus);
            // Clear the epsilon quarks in nucles list when the puzzle has been completed
            _epsilonQuarksInNucleus.Clear();
            // Play baryon appear animation
            _nucluesBaryonAnimator.SetTrigger("Spawn");
            // Trigger the OnPuzzleComplete event
            _epsilonManager.PuzzleComplete();
        }
    }

    private int GetBaryonCharge()
    {
        switch (_desiredBaryon)
        {
            case Baryon.Proton:
                return 9;
            case Baryon.Neutron:
                return 0;
            default:
                Debug.LogError("Baryon invalid or not specified");
                return -1;
        }
    } 

    private void DestroyParticlesInNucleus(List<EpsilonBaryon> baryons)
    {
        // Destroy all baryons in the baryons in nucleus list
        foreach (EpsilonBaryon baryon in baryons)
        {
            GameObject baryonGO = baryon.gameObject;
            Destroy(baryonGO);
        }
    }

    private void DestroyParticlesInNucleus(List<EpsilonQuark> quarks)
    {
        // Destroy all quarks in the quarks in nucleus list
        foreach (EpsilonQuark quark in quarks)
        {            
            GameObject quarkGO = quark.gameObject;
            Destroy(quarkGO);
        }
    }
}
