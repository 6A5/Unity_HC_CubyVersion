using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseUI;

    /// <summary>
    /// 遊戲的進行狀態
    /// </summary>
    public enum GameState
    {
        Pause,
        Going
    }

    public GameState gS; // 遊戲進行狀態 的變數

    public void StartGame()
    {
        gS = GameState.Going;
    }

    /// <summary>
    /// 通關遊戲或是暫停遊戲
    /// </summary>
    public void FinishGame_Or_PauseGame()
    {
        gS = GameState.Pause;
    }

    public void PauseGame()
    {
        if (gS == PauseSystem.GameState.Pause)
        {
            gS = GameState.Going;
            Time.timeScale = 1;
            pauseUI.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (gS == PauseSystem.GameState.Going)
        {
            gS = GameState.Pause;
            Time.timeScale = 0;
            pauseUI.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

}
