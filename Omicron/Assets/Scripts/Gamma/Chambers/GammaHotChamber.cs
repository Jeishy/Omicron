using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaHotChamber : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    [HideInInspector] public int HotParticlesInChamber;

    private void Start()
    {
        _gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
    }
    
    private void OnTriggerEnter(Collider col) 
    {
        GammaParticle gammaParticle = col.gameObject.GetComponent<GammaParticle>();
        // Set is particle in correct chambebr bool to true if it is hot
        if (gammaParticle.IsHot)
        {
            Debug.Log("Hot particle entered");
            gammaParticle.IsParticleInCorrectChamber = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //Debug.Log(gameObject.name + " in correct chamber");
        GammaParticle gammaParticle = col.gameObject.GetComponent<GammaParticle>();
        // Set is particle in correct chambebr bool to false if it has left the chamber
        if (gammaParticle.IsHot)
        {
            gammaParticle.IsParticleInCorrectChamber = false;
        }
    }
}
