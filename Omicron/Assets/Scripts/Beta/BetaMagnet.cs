using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds functions for magnetism calculations
// All magnets inherit this class
public class BetaMagnet : MonoBehaviour
{
    [Tooltip ("Sets polarity of magnet (TRUE = North, FALSE = South)")]
    [HideInInspector] public bool magnetPolarity;       // The polarity of the magnet
    private float maxMagnetStrength = 0.3f;             // The max strength of forces magnet exerts on other magnets
    public float magnetStrength;                        // Magnets field strength
    public float maxRadius;                             // Max radius of magnets influence
    [HideInInspector] public Collider[] magnetsInRange; // Array that stores all magnets in range


    // Function for attracting target magnet along direcition vector between target magnet and itself
    public virtual void Attract(Rigidbody magnet, Rigidbody targetMagnet, Vector3 direction)
    {
        float force = CalculateForce(magnet, targetMagnet);
        targetMagnet.AddForce(-direction * force);
    }

    // Function for repelling target magnet along direcition vector between target magnet and itself
    public virtual void Repel(Rigidbody magnet, Rigidbody targetMagnet, Vector3 direction)
    {
        float force = CalculateForce(magnet, targetMagnet);
        targetMagnet.AddForce(direction * force);
    }

    // Function for calculating force between magnet and target magnet
    // This function uses Newton's Law of Universal Gravitation
    // Reference: https://en.wikipedia.org/wiki/Newton%27s_law_of_universal_gravitation
    public virtual float CalculateForce(Rigidbody magnet, Rigidbody targetMagnet)
    {
        float distance = Vector3.Distance(magnet.position, targetMagnet.position);
        float force = (magnet.mass * targetMagnet.mass)/Mathf.Pow(distance, 2);
        // Clamps force if calculated force is higher than specified max strength of magnets
        if (force > maxMagnetStrength)
            force = maxMagnetStrength;
        return force;
    }

    // Finds all magnets in the Magnet layer, in a sphere around the magnet
    // Uses distance parsed in a max radius of sphere
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
