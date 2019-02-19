using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldOver : MonoBehaviour
{
    private HubWorldManager hubManager;

    private void OnEnable()
    {
        Setup();
        hubManager.OnOver += Over;
    }

    private void OnDisable()
    {
        hubManager.OnOver -= Over;
    }

    private void Setup()
    {
        hubManager = GetComponent<HubWorldManager>();
    }

    private void Over(Collider uiElement)
    {
        Animator anim = uiElement.GetComponent<Animator>();
        anim.SetTrigger("Increase");
   }
}
