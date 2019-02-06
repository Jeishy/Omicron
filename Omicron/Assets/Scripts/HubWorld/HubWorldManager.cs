using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldManager : MonoBehaviour
{
    #region Delegates and Events
    public delegate void HubWorldLevelSelectHandler(string level);
    public event HubWorldLevelSelectHandler OnLevelSelect;

    public delegate void HubWorldInputHandler();
    public event HubWorldInputHandler OnOver;
    public event HubWorldInputHandler OnExit;
    public event HubWorldInputHandler OnOptions;
    #endregion


    // Function for running all methods subcribed to the OnLevelSelect event
    public void LevelSelect(string level)
    {
        if (OnLevelSelect != null)
        {
            OnLevelSelect(level);
        }
    }

    // Function for running all methods subcribed to the OnOver event
    public void Over()
    {
        if (OnOver != null)
        {
            OnOver();
        }
    }

    // Function for running all methods subcribed to the OnExit event
    public void Exit()
    {
        if (OnExit != null)
        {
            OnExit();
        }
    } 

    // Function for running all methods subcribed to the OnOptions event
    public void Options()
    {
        if (OnOptions != null)
        {
            OnOptions();
        }
    }     
}
