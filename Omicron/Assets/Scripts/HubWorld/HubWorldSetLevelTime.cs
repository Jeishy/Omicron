using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HubWorldSetLevelTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeTakenText;

    // Start is called before the first frame update
    void Start()
    {
        float timeTaken = GameManager.Instance.GetCompleteLevelTime(GameManager.Instance.LevelStringToLevelType(gameObject.name));
        int minutes = Mathf.RoundToInt(timeTaken / 60f);
        if (minutes >= 1)
            _timeTakenText.text = "Time Taken: " + minutes + " minutes!";
        else
        {
            _timeTakenText.text = "Time Taken: " + Mathf.RoundToInt(timeTaken) + " seconds!";
        }
    }
}
