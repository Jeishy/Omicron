using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaColdChamber : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    [HideInInspector] public int ColdParticlesInChamber;

    private void Start()
    {
        _gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("GammaParticle"))
        {
            //Debug.Log(gameObject.name + " in correct chamber");
            GammaParticle gammaParticle = col.gameObject.GetComponent<GammaParticle>();
            // Set is particle in correct chambebr bool to true if it is cold
            if (!gammaParticle.IsHot)
            {
                Debug.Log("Cold particle entered");
                gammaParticle.IsParticleInCorrectChamber = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("GammaParticle"))
        {
            //Debug.Log(gameObject.name + " in correct chamber");
            GammaParticle gammaParticle = col.gameObject.GetComponent<GammaParticle>();
            // Set is particle in correct chambebr bool to false if it has left the chamber
            if (!gammaParticle.IsHot)
            {
                gammaParticle.IsParticleInCorrectChamber = false;
            }
        }
    }
}
