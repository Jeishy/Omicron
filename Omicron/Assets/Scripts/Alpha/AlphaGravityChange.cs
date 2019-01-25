using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaGravityChange : MonoBehaviour
{
    private AlphaLevelManager alphaManager;
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
        alphaManager.IsGravityChanged = true;
        //parabolicRaycaster.acceleration = -gravity.y;
        //Debug.Log(parabolicRaycaster.acceleration);
        Physics.gravity = gravity;
    }
}