using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedPanelOver : MonoBehaviour
{
    private LevelCompletedManager _levelCompletedManager;

    private void OnEnable() 
    {
        Setup();
        _levelCompletedManager.OnOver += Over;
    }

    private void OnDisable() 
    {
        _levelCompletedManager.OnOver -= Over;        
    }

    private void Setup()
    {
        _levelCompletedManager = GetComponent<LevelCompletedManager>();
    }

    private void Over(Collider panel)
    {
        // Increase the size of the panel that was selected
        Animator anim = panel.GetComponent<Animator>();
        anim.SetTrigger("Increase");
    }
}
