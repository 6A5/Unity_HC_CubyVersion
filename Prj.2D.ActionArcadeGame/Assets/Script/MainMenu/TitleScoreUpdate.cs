using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScoreUpdate : MonoBehaviour
{
    [SerializeField] Text txtHScore;
    [SerializeField] Text txtScore;

    /// <summary>
    ///  用來更新 Title 畫面上的分數
    /// </summary>
    private void Start()
    {
        if (HighScoreData.highScore == 0) // 如果沒有 最高分 就變成 5000
        {
            HighScoreData.highScore = 5000;
        }
        if (InGameData.thisTurn_Score > HighScoreData.highScore)
        {
            HighScoreData.highScore = InGameData.thisTurn_Score;
        }
        txtHScore.text = HighScoreData.highScore.ToString();
        txtScore.text = InGameData.thisTurn_Score.ToString();

    }
}
