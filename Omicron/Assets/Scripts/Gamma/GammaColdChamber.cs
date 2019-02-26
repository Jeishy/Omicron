using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaColdChamber : MonoBehaviour
{
    private GammaLevelManager gammaManager;
    [HideInInspector] public int ColdParticlesInChamber;

    private void Start()
    {
        gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        bool isParticleHot = col.GetComponent<GammaParticle>().IsHot;
        if (!isParticleHot)
        {
            ColdParticlesInChamber++;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        bool isParticleHot = col.GetComponent<GammaParticle>().IsHot;
        if (!isParticleHot)
        {
            ColdParticlesInChamber--;
        }
    }
}
