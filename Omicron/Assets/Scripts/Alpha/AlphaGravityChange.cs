using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaGravityChange : MonoBehaviour
{
    private AlphaLevelManager alphaManager;
    // Reference to parabolicRaycaster, used to see parabolic curve ball will follow
    [SerializeField] private ParabolicRaycaster parabolicRaycaster;

    private void OnEnable()
    {
        Setup();
        alphaManager.OnGravityChange += ChangeGravity;
    }

    private void OnDisable()
    {
        alphaManager.OnGravityChange -= ChangeGravity;
    }

    private void Setup()
    {
        alphaManager = GetComponent<AlphaLevelManager>();
    }

    private void ChangeGravity(Vector3 gravity)
    {
        // IsGravityChanged is flagged as true
        // This stops standard gravity being used
        alphaManager.IsGravityChanged = true;
        // Set gravity in game to parsed gravity
        Physics.gravity = gravity;
    }
}