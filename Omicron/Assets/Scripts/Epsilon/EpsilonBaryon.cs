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
        HasEnteredNucleus = false;
        SetParticleCharge(_baryon);
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
