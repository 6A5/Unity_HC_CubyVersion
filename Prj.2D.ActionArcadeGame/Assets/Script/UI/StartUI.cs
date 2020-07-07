using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] Text txtLevelName;
    [SerializeField] Text txtLife;
    [SerializeField] Text txtScore;
    [SerializeField] PauseSystem pS;
    [SerializeField] AudioSource aC;

    private void Awake()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        pS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseSystem>();
        aC = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    private void Start()
    {
        txtLevelName.text = SceneManager.GetActiveScene().name;
        txtLife.text = InGameData.thisTurn_Life.ToString();
        txtScore.text = InGameData.thisTurn_Score.ToString();

        Invoke("StartGame", 2f); // 一段時間後呼叫
    }

    /// <summary>
    /// 隱藏 UI 開始遊戲
    /// </summary>
    void StartGame()
    {
        pS.StartGame();
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        aC.Play();
    }
}
