using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonBaryon : EpsilonParticle
{
    [SerializeField] private Baryon _baryon;
    // Start is called before the first frame update
    void Start()
    {
        CanOrbit = false;
        SetParticleCharge(_baryon);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanOrbit)
        {
            // Orbit around the nuclues
            SetOrbit(transform, NucleusCentreTrans);
        }
    }
}
