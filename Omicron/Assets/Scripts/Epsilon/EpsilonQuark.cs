using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonQuark : EpsilonParticle
{
    [SerializeField] private Quark _quark;

    private Rigidbody _rb;

    // Start is called before the first frame update
    private void Start()
    {
        CanOrbit = false;
        // Set the charge of the quark
        SetParticleCharge(_quark);
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanOrbit)
        {
            if (!IsSpeedZero)
                DampSpeed(_rb);
                
            // Orbit around the nuclues if speed is 0
            if (_rb.velocity.magnitude <= 0f)
            {
                IsSpeedZero = true;
                SetOrbit(transform, NucleusCentreTrans, _rb);
            }
        }
    }
}
