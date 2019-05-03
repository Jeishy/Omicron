using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubWorldLevelSelect : MonoBehaviour
{
    private HubWorldManager hubManger;
    private void OnEnable()
    {
        Setup();
        hubManger.OnLevelSelect += LevelSelect;
    }

    private void OnDisable()
    {
        hubManger.OnLevelSelect -= LevelSelect;
    }

    private void Setup()
    {
        hubManger = GetComponent<HubWorldManager>();
    }

    private void LevelSelect(string selectedLevel)
    {
        // Play UI select sound
        AudioManager.Instance.Play("UISelect");
        // Call all methods subscribed to the OnLevelStart event
        GameManager.Instance.LevelStart();
        // Open the scene that is selected from the hub world
        // Scene transitions here, perhaps use a coroutine
        SceneManager.LoadScene(selectedLevel);
    }
}
