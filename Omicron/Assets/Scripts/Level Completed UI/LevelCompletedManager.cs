using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedManager : MonoBehaviour
{
    #region Delegates and Event
    public delegate void LevelCompletedEvent(Collider col);
    public event LevelCompletedEvent OnOver;
    public event LevelCompletedEvent OnExit;
    public event LevelCompletedEvent OnSelect;
    #endregion

    // Function for running all methods subcribed to the OnOver event
    public void Over(Collider col)
    {
        if (OnOver != null)
        {
            OnOver(col);
        }
    }

    // Function for running all methods subcribed to the OnExit event
    public void Exit(Collider col)
    {
        if (OnExit != null)
        {
            OnExit(col);
        }
    } 

    // Function for running all methods subcribed to the OnExit event
    public void Select(Collider col)
    {
        if (OnSelect != null)
        {
            OnSelect(col);
        }
    }
}
