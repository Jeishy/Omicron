using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaMagnets : MonoBehaviour
{
    [Tooltip ("Sets polarity of magnet (TRUE = North, FALSE = South)")]
    [SerializeField] private bool magnetPolarity;
    [SerializeField] [Range(0.01f, 10.0f)]private float magnetStrength;
    [SerializeField] [Range(0.1f, 30.0f)] private float maxRadius;
    private Collider[] magnetsInRange;

    // Update is called once per frame
    private void FixedUpdate()
    {
        FindAllMagnetsInRange(maxRadius);

        if (magnetPolarity == true)
            NorthMagnet(magnetStrength);
        else if (magnetPolarity == false)
            SouthMagnet(magnetStrength);
    }

    private void FindAllMagnetsInRange(float distance)
    {
        int layerMask = 1 << 10;
        magnetsInRange = Physics.OverlapSphere(transform.position, distance, layerMask);
    } 

    private void NorthMagnet(float magStrength)
    {
        foreach (Collider magnet in magnetsInRange)
        {
            Rigidbody magnetRB = magnet.GetComponent<Rigidbody>();
            Vector3 magnetPos = magnetRB.position;
            Vector3 direction = Vector3.Normalize(magnetPos - transform.position);
            bool isNorth = magnet.GetComponent<BetaMagnets>().magnetPolarity;

            if (isNorth)
            {
                Repel(magnetRB, direction, magStrength);
            }
            else
            {
                Attract(magnetRB, direction, magStrength);
            }
        }
    }

    private void SouthMagnet(float magStrength)
    {
        foreach (Collider magnet in magnetsInRange)
        {
            Rigidbody magnetRB = magnet.GetComponent<Rigidbody>();
            Vector3 magnetPos = magnetRB.position;
            Vector3 direction = Vector3.Normalize(magnetPos - transform.position);
            bool isNorth = magnet.GetComponent<BetaMagnets>().magnetPolarity;

            if (isNorth)
            {
                Attract(magnetRB, direction, magStrength);
            }
            else
            {
                Repel(magnetRB, direction, magStrength);
            }
        }
    }

    private void Attract(Rigidbody magnet, Vector3 direction, float strength)
    {
        magnet.AddForce(-direction * strength, ForceMode.Acceleration);
    }

    private void Repel(Rigidbody magnet, Vector3 direction, float strength)
    {
        magnet.AddForce(direction * strength, ForceMode.Acceleration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}
