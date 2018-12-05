using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void GameEventManager();
    public event GameEventManager OnGamePaused;
    public event GameEventManager OnGameQuit;
    public event GameEventManager OnNextLevel;
    public event GameEventManager OnRestartLevel;

    public void GamePaused()
    {
        if (OnGamePaused != null)
        {
            OnGamePaused();
        }
    }

    public void GameQuit()
    {
        if (OnGameQuit != null)
        {
            OnGameQuit();
        }
    }

    public void NextLevel()
    {
        if (OnNextLevel != null)
        {
            OnNextLevel();
        }
    }

    public void RestartLevel()
    {
        if (OnRestartLevel != null)
        {
            OnRestartLevel();
        }
    }
}
