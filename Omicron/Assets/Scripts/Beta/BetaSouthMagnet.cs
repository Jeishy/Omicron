using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaSouthMagnet : BetaMagnet
{
    private void Start()
    {
        // Setting mass to determine magnet's strength, due to using Newton's law of universal gravitation equation
        GetComponent<Rigidbody>().mass = magnetStrength;
        // magnetPolarity is set to south
        magnetPolarity = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Find all magnets in range of magnet, with specified radius
        FindAllMagnetsInRange(maxRadius);
        // If there is a magnet (that isnt the magnet itself)
        // Run NorthMagnet method
        if (magnetsInRange.Length > 1)
        {
            SouthMagnet();
        }
    }

    private void SouthMagnet()
        {
            // For every magnet in magnetsInRange array
            // Cache their stats and check if they are a North or South magnet
            // Attract or Repel respectively
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

                    bool magnetPolarity = targetMagnet.GetComponent<BetaMagnet>().magnetPolarity;

                    if (magnetPolarity == true)
                    {
                        Attract(magnetRB, targetMagnetRB, direction);
                    }
                    else if (magnetPolarity == false)
                    {
                        Repel(magnetRB, targetMagnetRB, direction);
                    }
                }
            }
        }   
}
