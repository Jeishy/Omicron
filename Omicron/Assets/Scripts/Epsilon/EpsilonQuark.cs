﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonQuark : EpsilonParticle
{
    [SerializeField] private Quark _quark;
    // Start is called before the first frame update
    private void Start()
    {
        CanOrbit = false;
        SetParticleCharge(_quark);
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanOrbit)
        {
            // Orbit around the nuclues
            SetOrbit(transform, NucleusCentreTrans);
        }
    }
}
