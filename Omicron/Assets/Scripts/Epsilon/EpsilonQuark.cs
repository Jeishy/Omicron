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
        HasEnteredNucleus = false;
        // Set the charge of the quark
        SetParticleCharge(_quark);
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (HasEnteredNucleus)
        {
            if (!IsSpeedZero)
            {
                DampSpeed(_rb);
                if (_rb.velocity.magnitude <= 0.5f)
                {
                    IsSpeedZero = true;
                    _rb.velocity = Vector3.zero;
                }
            }             
        }
    }
}
