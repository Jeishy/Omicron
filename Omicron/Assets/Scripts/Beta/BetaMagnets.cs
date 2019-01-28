using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaMagnets : MonoBehaviour
{
    [Tooltip ("Sets polarity of magnet (TRUE = North, FALSE = South)")]
    [SerializeField] private bool magnetPolarity;
    public float magnetStrength;
    public float maxRadius;
    private Collider[] magnetsInRange;

    // Update is called once per frame
    private void Update()
    {
        FindAllMagnetsInRange(maxRadius);

        if (magnetPolarity == true)
            NorthMagnet();
        else if (magnetPolarity == false)
            SouthMagnet(magnetStrength);
    }

    private void FindAllMagnetsInRange(float distance)
    {
        int layerMask = 1 << 10;
        magnetsInRange = Physics.OverlapSphere(transform.position, distance, layerMask);
    } 

    private void NorthMagnet()
    {
        if (magnetsInRange.Length > 1)
        {
            foreach (Collider targetMagnet in magnetsInRange)
            {
                Transform targetMagnetTrans = targetMagnet.GetComponent<Transform>();
                if (targetMagnetTrans.root != transform)
                {
                    Rigidbody magnetRB = targetMagnet.GetComponent<Rigidbody>();
                    Rigidbody targetMagnetRB = targetMagnet.GetComponent<Rigidbody>();

                    Vector3 targetMagnetPos = magnetRB.position;
                    Vector3 magnetPos = transform.position;
                    Vector3 direction = Vector3.Normalize(targetMagnetPos - magnetPos);

                    float targetMagnetFieldStrength = targetMagnet.GetComponent<BetaMagnets>().magnetStrength;

                    bool isNorth = targetMagnet.GetComponent<BetaMagnets>().magnetPolarity;

                    if (isNorth)
                    {
                        Repel(magnetRB, targetMagnetRB, direction);
                    }
                    else
                    {
                        Attract(magnetRB, targetMagnetRB, direction);
                    }
                }
            }
        }
    }

    private void SouthMagnet(float magStrength)
    {
        if (magnetsInRange.Length > 1)
        {
            foreach (Collider targetMagnet in magnetsInRange)
            {
                Transform targetMagnetTrans = targetMagnet.GetComponent<Transform>();
                if (targetMagnetTrans.root != transform)
                {
                    Rigidbody magnetRB = GetComponent<Rigidbody>();
                    Rigidbody targetMagnetRB = targetMagnet.GetComponent<Rigidbody>();

                    Vector3 targetMagnetPos = targetMagnetRB.position;
                    Vector3 magnetPos = transform.position;
                    Vector3 direction = Vector3.Normalize(targetMagnetPos - magnetPos);

                    // Setting mass to determine magnet's strength, due to using Newton's law of universal gravitation equation
                    GetComponent<Rigidbody>().mass = magStrength;

                    bool isNorth = targetMagnet.GetComponent<BetaMagnets>().magnetPolarity;

                    if (isNorth)
                    {
                        Attract(magnetRB, targetMagnetRB, direction);
                    }
                    else
                    {
                        Repel(magnetRB, targetMagnetRB, direction);
                    }
                }
            }
        }
    }

    private void Attract(Rigidbody magnet, Rigidbody targetMagnet, Vector3 direction)
    {
        float force = CalculateForce(magnet, targetMagnet);
        magnet.AddForce(-direction * force);
    }

    private void Repel(Rigidbody magnet, Rigidbody targetMagnet, Vector3 direction)
    {
        float force = CalculateForce(magnet, targetMagnet);
        magnet.AddForce(direction * force);
    }

    public float CalculateForce(Rigidbody magnet, Rigidbody targetMagnet)
    {
        float distance = Vector3.Distance(magnet.position, targetMagnet.position);
        float force = (magnet.mass * targetMagnet.mass)/Mathf.Pow(distance, 2);
        if (force > 0.2f)
            force = 0.2f;
        Debug.Log(force);
        return force;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}
