using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldManager : MonoBehaviour
{
    #region Delegates and Events
    public delegate void HubWorldLevelSelectHandler(string level);
    public event HubWorldLevelSelectHandler OnLevelSelect;

    public delegate void HubWorldInputHandler(Collider col);
    public event HubWorldInputHandler OnOver;
    public event HubWorldInputHandler OnExit;
    public event HubWorldInputHandler OnOptions;
    #endregion

    [HideInInspector] public bool IsEnlarged;


    // Function for running all methods subcribed to the OnLevelSelect event
    public void LevelSelect(string level)
    {
        if (OnLevelSelect != null)
        {
            OnLevelSelect(level);
        }
    }

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

    // Function for running all methods subcribed to the OnOptions event
    public void Options(Collider col)
    {
        if (OnOptions != null)
        {
            OnOptions(col);
        }
    }     
}
