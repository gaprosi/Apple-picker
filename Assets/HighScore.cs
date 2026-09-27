using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static private Text _UI_TEXT;
    static private int _SCORE = 1000;

    void Awake()
    {
        _UI_TEXT = GetComponent<Text>();

        // If a saved high score exists, load it
        if (PlayerPrefs.HasKey("HighScore"))
        {
            SCORE = PlayerPrefs.GetInt("HighScore");
        }

        // Make sure HighScore exists in PlayerPrefs
        PlayerPrefs.SetInt("HighScore", SCORE);
    }

    static public int SCORE
    {
        get
        {
            return _SCORE;
        }

        private set
        {
            _SCORE = value;

            // Save the high score
            PlayerPrefs.SetInt("HighScore", value);

            if (_UI_TEXT != null)
            {
                _UI_TEXT.text =
                    "High Score: " + value.ToString("#,0");
            }
        }
    }

    static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    {
        if (scoreToTry <= SCORE)
        {
            return;
        }

        SCORE = scoreToTry;
    }

    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;

            PlayerPrefs.SetInt("HighScore", 1000);

            Debug.LogWarning(
                "PlayerPrefs HighScore reset to 1,000."
            );
        }
    }
}