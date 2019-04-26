﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quark
{
    Up, Down
}

public enum Baryon
{
    Proton, Neutron
}

public class EpsilonParticle : MonoBehaviour
{
    [HideInInspector] public Transform NucleusCentreTrans;
    [HideInInspector] public int Charge;
    [HideInInspector] public bool CanOrbit;
    [HideInInspector] public bool IsSpeedZero;

    public float OrbitSpeed;
    [Range(0.01f, 1.0f)] public float DampenTime;

    private Vector3 _vel;
    private float _rotateAngle;

    private void Start() 
    {
        _rotateAngle = 0f;
        IsSpeedZero = false;
    }

    // Function for setting the charge of a quark
    public virtual void SetParticleCharge(Quark quark)
    {
        switch (quark)
        {
            case Quark.Up:
                Charge = 6;
                break;
            case Quark.Down:
                Charge = -3;
                break;
        }
    }

    // Function for setting the charge of a baryon
    public virtual void SetParticleCharge(Baryon baryon)
    {
        switch (baryon)
        {
            case Baryon.Proton:
                Charge = 9;
                break;
            case Baryon.Neutron:
                Charge = 0;
                break;
        }
    }

    // Function for causing a particle to orbit the centre of a nucleus
    public virtual void SetOrbit(Transform particle,Transform nucleusCentreTrans, Rigidbody particleRB)
    {
        // Set velocity of particle to 0
        particleRB.velocity = Vector3.zero;
        // Set particle to rotate around nucleus' centre
        particle.RotateAround(nucleusCentreTrans.position, Vector3.up, OrbitSpeed * Time.deltaTime);
    }

    // Function for dampening a particle's speed to 0
    public virtual void DampSpeed(Rigidbody particleRb)
    {
        // Smooth velocity of particle down to 0 over a specified amount of time
        particleRb.velocity = Vector3.SmoothDamp(particleRb.velocity, Vector3.zero, ref _vel, DampenTime);
    }
}
