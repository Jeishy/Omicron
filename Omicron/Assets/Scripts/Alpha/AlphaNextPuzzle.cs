using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaNextPuzzle : MonoBehaviour
{
    [SerializeField] private AlphaLevelManager alphaLevelManager;
    private GameManager gameManager;
    //[SerializeField] private Text debugText;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            gameManager.NextPuzzle();
            alphaLevelManager.ResetBallPosition();
        }
    }

    /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameManager.NextPuzzle();
            alphaLevelManager.ResetBallPosition();

        }
    }*/
}
