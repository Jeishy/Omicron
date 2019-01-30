using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaMagnet : MonoBehaviour
{
    [Tooltip ("Sets polarity of magnet (TRUE = North, FALSE = South)")]
    [HideInInspector] public bool magnetPolarity;
    private float maxMagnetStrength = 0.3f;
    public float magnetStrength;
    public float maxRadius;
    [HideInInspector] public Collider[] magnetsInRange;
    public virtual void Attract(Rigidbody magnet, Rigidbody targetMagnet, Vector3 direction)
    {
        float force = CalculateForce(magnet, targetMagnet);
        targetMagnet.AddForce(-direction * force);
    }

    public virtual void Repel(Rigidbody magnet, Rigidbody targetMagnet, Vector3 direction)
    {
        float force = CalculateForce(magnet, targetMagnet);
        targetMagnet.AddForce(direction * force);
    }

    public virtual float CalculateForce(Rigidbody magnet, Rigidbody targetMagnet)
    {
        float distance = Vector3.Distance(magnet.position, targetMagnet.position);
        float force = (magnet.mass * targetMagnet.mass)/Mathf.Pow(distance, 2);
        if (force > maxMagnetStrength)
            force = maxMagnetStrength;
        return force;
    }

    public virtual void FindAllMagnetsInRange(float distance)
    {
        int layerMask = 1 << 10;
        magnetsInRange = Physics.OverlapSphere(transform.position, distance, layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    } 
}
