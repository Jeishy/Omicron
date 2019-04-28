using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonBaryon : EpsilonParticle
{
    [SerializeField] private Baryon _baryon;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        CanOrbit = false;
        SetParticleCharge(_baryon);
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanOrbit)
        {
            if (!IsSpeedZero)
                DampSpeed(_rb);
                
            // // Orbit around the nuclues if speed is 0
            // if (_rb.velocity.magnitude <= 0f)
            // {
            //     IsSpeedZero = true;
            //     SetOrbit(transform, NucleusCentreTrans, _rb);
            // }
        }
    }
}
