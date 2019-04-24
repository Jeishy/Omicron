using System.Collections;
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
    [HideInInspector] public float Charge;
    [HideInInspector] public bool CanOrbit;
    public float OrbitSpeed;

    private float rotateAngle;
    // Function for setting the charge of a quark
    public virtual void SetParticleCharge(Quark quark)
    {
        switch (quark)
        {
            case Quark.Up:
                Charge = 0.65f;
                break;
            case Quark.Down:
                Charge = -0.35f;
                break;
        }
    }

    // Function for setting the charge of a baryon
    public virtual void SetParticleCharge(Baryon baryon)
    {
        switch (baryon)
        {
            case Baryon.Proton:
                Charge = 1f;
                break;
            case Baryon.Neutron:
                Charge = 0f;
                break;
        }
    }

    // Function for causing a particle to orbit the centre of a nucleus
    public virtual void SetOrbit(Transform particle,Transform nucleusCentreTrans)
    {
        // Set particle to rotatae around nucleus' centre
        particle.RotateAround(nucleusCentreTrans.position, Vector3.up, OrbitSpeed * Time.deltaTime);
    }
}
