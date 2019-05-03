using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounceSound : MonoBehaviour
{
    private void OnCollisionEnter(Collision coll) 
    {
        Collider col = coll.gameObject.GetComponent<Collider>();
        if (col.CompareTag("Ball"))
        {
            // Play bounce sound when ball enters bounce pad's collider
            AudioManager.Instance.Play("AlphaBounce");
        }
    }
}
