using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HubWorldSetLevelTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeTakenText;

    // Start is called before the first frame update
    void Start()
    {
        float timeTaken = GameManager.Instance.GetCompleteLevelTime(GameManager.Instance.LevelStringToLevelType(gameObject.name));
        Debug.Log("Time taken for " + gameObject.name + ": " + timeTaken + "s");
        int minutes = Mathf.RoundToInt(timeTaken / 60f);
        if (minutes >= 1)
        {
            if (minutes == 1)
                _timeTakenText.text = "Time Taken: " + minutes + " minute  " + (timeTaken - 60f) + " seconds!";
            else if (minutes >= 2)
                _timeTakenText.text = "Time Taken: " + minutes + " minutes " + (timeTaken - 60f*minutes) + " seconds!";
        }
        else
        {
            _timeTakenText.text = "Time Taken: " + Mathf.RoundToInt(timeTaken) + " seconds!";
        }
    }
}
