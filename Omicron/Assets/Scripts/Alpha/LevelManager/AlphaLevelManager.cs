using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaLevelManager : MonoBehaviour {

    public delegate void AlphaLevelEventManager();
    public event AlphaLevelEventManager OnNextPuzzle;
    public event AlphaLevelEventManager OnPuzzleRestart;
    public event AlphaLevelEventManager OnPuzzleFail;

    public delegate void AlphaLevelGravityChange(Vector3 gravity);
    public event AlphaLevelGravityChange OnGravityChange;

    [HideInInspector] public bool IsGravityChanged { get; private set; }

    public void NextPuzzle()
    {
        if (OnNextPuzzle != null)
        {
            OnNextPuzzle();
        }
    }

    public void PuzzleRestart()
    {
        if (OnPuzzleRestart != null)
        {
            OnPuzzleRestart();
        }
    }

    public void PuzzleFail()
    {
        if (OnPuzzleFail !=  null)
        {
            OnPuzzleFail();
        }
    }

    public void GravityChange(Vector3 gravity)
    {
        if (OnGravityChange != null)
        {
            IsGravityChanged = true;
            OnGravityChange(gravity);
        }
    }
}
