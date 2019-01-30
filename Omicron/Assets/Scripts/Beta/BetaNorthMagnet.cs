using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaNorthMagnet : BetaMagnet
{
    private void Start()
    {
        // Setting mass to determine magnet's strength, due to using Newton's law of universal gravitation equation
        GetComponent<Rigidbody>().mass = magnetStrength;
        // magnetPolarity is set to north
        magnetPolarity = true;
    }

    // Update is called once per frame
    private void Update()
    {
        FindAllMagnetsInRange(maxRadius);
        if (magnetsInRange.Length > 1)
        {
            NorthMagnet();
        }
    }

     private void NorthMagnet()
    {
        foreach (Collider targetMagnet in magnetsInRange)
        {
            Rigidbody magnetRB = targetMagnet.GetComponent<Rigidbody>();
            Rigidbody targetMagnetRB = targetMagnet.GetComponent<Rigidbody>();

            Vector3 targetMagnetPos = magnetRB.position;
            Vector3 magnetPos = transform.position;
            Vector3 direction = Vector3.Normalize(targetMagnetPos - magnetPos);

            bool magnetPolarity = targetMagnet.GetComponent<BetaMagnet>().magnetPolarity;

            if (magnetPolarity == true)
            {
                Repel(magnetRB, targetMagnetRB, direction);
            }
            else if (magnetPolarity == false)
            {
                Attract(magnetRB, targetMagnetRB, direction);
            }
        }
    }
}
