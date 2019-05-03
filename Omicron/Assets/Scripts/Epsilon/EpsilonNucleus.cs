using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonNucleus : MonoBehaviour
{
    [HideInInspector] public bool IsParticleCreated;                                                                // A bool for flagging if  the desured particle in this nucleus has been created
    [HideInInspector] public List<EpsilonBaryon> EpsilonBaryonsInNucleus = new List<EpsilonBaryon>();              // A list of all baryons in the nuclues
    [HideInInspector] public List<EpsilonQuark> EpsilonQuarksInNucleus = new List<EpsilonQuark>();                 // A list of all quarks in the nucleus
 
    [SerializeField] private Baryon _desiredBaryon;                                                                 // The baryon that must be created in this nucleus
    [SerializeField] private Animator _nucluesBaryonAnimator;

   private int _chargeInNucleus;
    private EpsilonLevelManager _epsilonManager;

    private void Start() 
    {
        // Set charge in nucleus to be neutral (to 0)
        _chargeInNucleus = 0;
        _epsilonManager = GameObject.Find("EpsilonLevelManager").GetComponent<EpsilonLevelManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check if a particle has entered the nucleus
        if (col.gameObject.layer == 17)
        {
            // Set layer of particle to default so that it cannot be targetted
            col.gameObject.layer = 0;
            // Play particle enter sound
            AudioManager.Instance.Play("ParticleEnter");
            // Get reference to the particles epsilon particle component
            EpsilonParticle _epsilonParticle = col.gameObject.GetComponent<EpsilonParticle>();

            if (col.CompareTag("ShelfQuark"))
            {
                // Add the quark to the list of quarks in the nucleus
                EpsilonQuark epsilonQuark = col.gameObject.GetComponent<EpsilonQuark>();
                EpsilonQuarksInNucleus.Add(epsilonQuark);
                CheckQuarksInNucleus();
            }
            else if (col.CompareTag("ShelfBaryon"))
            {
                // Add the quark to the list of quarks in the nucleus
                EpsilonBaryon epsilonBaryon = col.gameObject.GetComponent<EpsilonBaryon>();
                EpsilonBaryonsInNucleus.Add(epsilonBaryon);
            }
            
            // Set HasEnteredNucleus bool to true
            _epsilonParticle.HasEnteredNucleus = true;
        }
    }

    private void CheckQuarksInNucleus()
    {
        _chargeInNucleus = 0;
        foreach (EpsilonQuark quark in EpsilonQuarksInNucleus)
        {
            // Get the sum charge of the nucleus
            _chargeInNucleus += quark.Charge;
        }

        // Get the charge of the baryon
        int bayronCharge = GetBaryonCharge();

        if (_chargeInNucleus == bayronCharge)
        {
            // Destroy all quarks in nucleus
            StartCoroutine(WaitToDestroyParticlesInNucleus(EpsilonQuarksInNucleus));
            // Play baryon appear animation
            _nucluesBaryonAnimator.SetTrigger("Spawn");
            // Set is particle created bool to true
            IsParticleCreated = true;
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

    public IEnumerator WaitToDestroyParticlesInNucleus(List<EpsilonQuark> quarks)
    {
        yield return new WaitForSeconds(_epsilonManager.TimeTillParticlesDestroyed);
        // Destroy all quarks in the quarks in nucleus list
        foreach (EpsilonQuark quark in quarks)
        {            
            GameObject quarkGO = quark.gameObject;
            Destroy(quarkGO);
        }

        // Clear the epsilon quarks in nucles list when the puzzle has been completed
        EpsilonQuarksInNucleus.Clear();
    }

    public void DestroyParticlesInNucleus(List<EpsilonQuark> quarks)
    {
        // Destroy all quarks in the quarks in nucleus list
        foreach (EpsilonQuark quark in quarks)
        {            
            GameObject quarkGO = quark.gameObject;
            Destroy(quarkGO);
        }

        // Clear the epsilon quarks in nucles list when the puzzle has been completed
        EpsilonQuarksInNucleus.Clear();
    }

    public void DestroyParticlesInNucleus(List<EpsilonBaryon> baryons)
    {
        // Destroy all baryons in the baryons in nucleus list
        foreach (EpsilonBaryon baryon in baryons)
        {
            GameObject baryonGO = baryon.gameObject;
            Destroy(baryonGO);
        }

        // Clear the epsilon quarks in nucles list when the puzzle has been completed
        EpsilonBaryonsInNucleus.Clear();
    }
}
