using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TMP_Text PreviousScore;

    private void Start()
    {
        // hide previous score if no score has been logged (Score == -1)
        if (Score.ScoreCount < 0)
        {
            PreviousScore.SetText("");
        }
        else
        {
            PreviousScore.SetText("Previous Score: " + Score.ScoreCount);
        }
    }

    // load game scene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
