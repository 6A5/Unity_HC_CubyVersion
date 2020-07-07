using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [Header("物件引用"), Tooltip("玩家物件，自動取得")]
    [SerializeField] PlayerControl player;
    [Header("UI 內物件")]
    [SerializeField] Text scoreText;
    [SerializeField] Text lifeText;
    [SerializeField] GameObject hpPoint1, hpPoint2, hpPoint3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update()
    {
        UI_HP_Update();
        UI_Score_Update();
        UI_Life_Update();
    }

    void UI_HP_Update() // 更新 HP 球
    {
        switch (player.cur_health)
        {
            case 3:
                hpPoint1.GetComponent<Image>().enabled = true;
                hpPoint2.GetComponent<Image>().enabled = true;
                hpPoint3.GetComponent<Image>().enabled = true;
                break;
            case 2:
                hpPoint1.GetComponent<Image>().enabled = true;
                hpPoint2.GetComponent<Image>().enabled = true;
                hpPoint3.GetComponent<Image>().enabled = false;
                break;
            case 1:
                hpPoint1.GetComponent<Image>().enabled = true;
                hpPoint2.GetComponent<Image>().enabled = false;
                hpPoint3.GetComponent<Image>().enabled = false;
                break;
            case 0:
                hpPoint1.GetComponent<Image>().enabled = false;
                hpPoint2.GetComponent<Image>().enabled = false;
                hpPoint3.GetComponent<Image>().enabled = false;
                break;
        }
    }

    void UI_Score_Update() // 更新 分數
    {
        scoreText.text = player.score.ToString();
    }

    void UI_Life_Update() // 更新 生命
    {
        lifeText.text = player.life.ToString();
    }
}
