using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;

    private Text uiText;
    private Text roundText;
    private ApplePicker applePicker;

    void Start()
    {
        uiText = GetComponent<Text>();

        GameObject roundGO = GameObject.Find("RoundText");

        if (roundGO != null)
        {
            roundText = roundGO.GetComponent<Text>();
        }

        applePicker = Camera.main.GetComponent<ApplePicker>();
    }

    void Update()
    {
        // Update score
        uiText.text = score.ToString("#,0");

        // Don't overwrite "Game Over"
        if (applePicker != null && applePicker.gameOver)
        {
            return;
        }

        // Determine round
        int round = (score / 1000) + 1;

        if (round > 4)
        {
            round = 4;
        }

        if (roundText != null)
        {
            roundText.text = "Round " + round;
        }
    }
}