using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStandardGravity : MonoBehaviour {
    [HideInInspector] private float _gravity;
    public float Gravity { get{ return _gravity; } }

    private Rigidbody ballRB;


    public PlayerManager PlayerManager;
    public AlphaLevelManager AlphaManager;

    // Use this for initialization
    void Start () {
        ballRB = GetComponent<Rigidbody>();
        //ballRB.isKinematic = (!PlayerManager.IsBallThrown)
	}
	
	// Update is called once per frame
	void Update () {
        if (!AlphaManager.IsGravityChanged)
            StandardGravity();
	}

    void StandardGravity()
    {
        Physics.gravity = new Vector3(0, Gravity, 0);
    }
}
