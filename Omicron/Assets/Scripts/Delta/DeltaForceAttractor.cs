using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaForceAttractor : MonoBehaviour
{
    [SerializeField] private float _attractorStrength;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _photonAddTime;

    private Rigidbody _rb;
    private const float _maxStrength = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find photons in range
        Collider[] photonsInRange = FindPhotonsInRange();
        if (photonsInRange.Length > 0)
        {
            // Play force attractor sound for as long as there is a photon in range
            AudioManager.Instance.Play("ForceAttractor");
            // Apply attraction force to all photons in range
            foreach (Collider photon in photonsInRange)
            {
                Rigidbody photonRB = photon.GetComponent<Rigidbody>();
                Attraction(photonRB);
                DeltaPhotonTimer photonTimer = photon.GetComponent<DeltaPhotonTimer>();
                
                // If the photon has never been in range, and therefore, had its max time increased, increase the max photon time
                if (!photonTimer.HasMaxPhotonTimeIncreased)
                {
                    photonTimer.HasMaxPhotonTimeIncreased = true;
                    photonTimer.AddTime(_photonAddTime);
                }

                // If the photon is within a certain distance of the force attractor, destroy it >:)
                float distance = Vector3.Distance(transform.position, photon.transform.position);
                if (distance <= 0.7f)
                {
                    // Play a particle effect
                    //
                    Destroy(photon.gameObject);
                }
            }
        }
    }

    // This function uses Newton's Law of Universal Gravitation
    // Reference: https://en.wikipedia.org/wiki/Newton%27s_law_of_universal_gravitation
    private void Attraction(Rigidbody photonRB)
    {
        // Caculate separation between photon and obstacle
        float distance = Vector3.Distance(_rb.position, photonRB.position);
        float force = (_rb.mass * photonRB.mass)/Mathf.Pow(distance, 2);
        // Clamps force if calculated force is higher than specified max strength of magnets
        if (force > _maxStrength)
            force = _maxStrength;
        // calculate direction vector between photon and obstacle
        Vector3 dir = Vector3.Normalize(photonRB.transform.position - transform.position);
        // Apply force to photon
        photonRB.AddForce(_attractorStrength * force * -dir, ForceMode.Force) ;
    }

    private Collider[] FindPhotonsInRange()
    {
        // Find all nearby photons
        Collider[] photons = Physics.OverlapSphere(transform.position, _range, _layerMask.value);
        return photons;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
