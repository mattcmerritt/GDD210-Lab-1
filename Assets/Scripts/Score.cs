using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TMP_Text ScoreText, LivesText;
    public static int ScoreCount = -1;
    public int StartingLives, Lives;

    private void Start()
    {
        ScoreCount = -1; // starts below so it can be incremented
        IncrementScore();
        Lives = StartingLives + 1; // starts above so it can be decremented
        DecrementLives();
    }

    public void IncrementScore()
    {
        ScoreCount++;
        ScoreText.SetText("Score: " + ScoreCount);
    }

    public void DecrementLives()
    {
        Lives--;
        if (Lives <= 0)
        {
            SceneManager.LoadScene(0); // load title screen
        }
        LivesText.SetText("Lives: " + Lives);
    }
}
