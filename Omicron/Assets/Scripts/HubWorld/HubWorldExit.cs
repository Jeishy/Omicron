using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldExit : MonoBehaviour
{
    private HubWorldManager hubManager;

    private void OnEnable()
    {
        Setup();
        hubManager.OnExit += Exit;
    }

    private void OnDisable()
    {
        hubManager.OnExit -= Exit;
    }

    private void Setup()
    {
        hubManager = GetComponent<HubWorldManager>();
    }

    private void Exit(Collider uiElement)
    {
        // Decrease the size of the panel that was selected
        Animator anim = uiElement.GetComponent<Animator>();
        anim.SetTrigger("Decrease");
   }
}

