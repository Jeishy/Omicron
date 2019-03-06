using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaHotChamber : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    [HideInInspector] public int HotParticlesInChamber;
    private int count;

    private void Start()
    {
        _gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        bool isParticleHot = col.GetComponent<GammaParticle>().IsHot;
        if (isParticleHot)
        {
            HotParticlesInChamber++;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        bool isParticleHot = col.GetComponent<GammaParticle>().IsHot;
        if (isParticleHot)
        {
            HotParticlesInChamber--;
        }
    }
}
