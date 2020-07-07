using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
    [SerializeField] Text txtBonusScore; // 文字
    [SerializeField] AudioSource aC;

    public int intBonusScore; // 過關獲得分數

    private void Awake()
    {
        aC = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    private void Start()
    {
        txtBonusScore.text = intBonusScore.ToString();
    }

    /// <summary>
    /// 到終點顯示 UI
    /// </summary>
    public void FinishGame()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        aC.Stop();
    }
}
