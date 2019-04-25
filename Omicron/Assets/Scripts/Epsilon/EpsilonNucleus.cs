using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonNucleus : MonoBehaviour
{
    [SerializeField] private EpsilonBaryon _desiredBaryon;

    private List<EpsilonQuark> _epsilonQuarksInNucleus = new List<EpsilonQuark>();
    private List<EpsilonBaryon> _epsilonBaryonsInNucleus = new List<EpsilonBaryon>();
    private float _chargeInNucleus;
    private EpsilonLevelManager _epsilonManager;

    private void Start() 
    {
        // Set charge in nucleus to be neutral (to 0)
        _chargeInNucleus = 0f;
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
                Debug.Log("Shelf quark in range");
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
        _chargeInNucleus = 0f;
        foreach (EpsilonQuark quark in _epsilonQuarksInNucleus)
        {
            // Get the sum charge of the nucleus
            _chargeInNucleus += quark.Charge;
        }

        // if (_chargeInNucleus == _desiredBaryon.Charge)
        // {
        //     // Clear the epsilon quarks in nucles list when the puzzle has been completed
        //     _epsilonQuarksInNucleus.Clear();
        //     // Trigger the OnPuzzleComplete event
        //     _epsilonManager.PuzzleComplete();
        // }
    } 
}
