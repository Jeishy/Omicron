using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedPanelExit : MonoBehaviour
{
    private LevelCompletedManager _levelCompletedManager;

    private void OnEnable() 
    {
        Setup();
        _levelCompletedManager.OnExit += Exit;
    }

    private void OnDisable() 
    {
        _levelCompletedManager.OnExit -= Exit;
    }

    private void Setup()
    {
        _levelCompletedManager = GetComponent<LevelCompletedManager>();
    }

    private void Exit(Collider panel)
    {
        // Decrease the size of the panel that was selected
        Animator anim = panel.GetComponent<Animator>();
        anim.SetTrigger("Decrease");
    }
}
