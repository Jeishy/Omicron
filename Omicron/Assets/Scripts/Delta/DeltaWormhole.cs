using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaWormhole : MonoBehaviour
{
    [SerializeField] private float _photonAddTime;
    [SerializeField] private Transform _exitColTrans;
    [SerializeField] private Transform _directionTrans;

    private DeltaPhotonShot _deltaPhotonShot;

    private void Start()
    {
        _deltaPhotonShot = GameObject.Find("DeltaLevelManager").GetComponent<DeltaPhotonShot>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Photon"))
        {
            // Play wormhole sound
            AudioManager.Instance.Play("Wormhole");
            // Add time to photon timer
            AddTimeToPhoton(col);
            // Send proton to exit collider position
            Transform colTrans = col.gameObject.transform;
            colTrans.position = _exitColTrans.position;
            // Apply velocity in direction from exit colliders centre pos and direction pos
            Rigidbody colRB = col.gameObject.GetComponent<Rigidbody>();
            float photonShotForce = _deltaPhotonShot.ShotForce;
            Vector3 direction = Vector3.Normalize(_directionTrans.position - _exitColTrans.position);
            Vector3 newVelocity = direction * photonShotForce;
            colRB.velocity = newVelocity;
        }
    }

    private void AddTimeToPhoton(Collider photon)
    {
        Rigidbody photonRB = photon.GetComponent<Rigidbody>();
        DeltaPhotonTimer photonTimer = photon.GetComponent<DeltaPhotonTimer>();
        
        // If the photon has never been in range, and therefore, had its max time increased, increase the max photon time
        if (!photonTimer.HasMaxPhotonTimeIncreased)
        {
            photonTimer.HasMaxPhotonTimeIncreased = true;
            photonTimer.AddTime(_photonAddTime);
        }
    }
}
