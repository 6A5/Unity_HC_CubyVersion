using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossWatcher : MonoBehaviour
{
    [Header("手動設定")]
    public GameObject boss;
    public GameObject finishUI;

    GameObject gM;
    bool a; // 開關 防止 Ivoke 多次執行
    BossAI bossAI;

    private void Start()
    {
        bossAI = boss.GetComponent<BossAI>();
        gM = GameObject.FindGameObjectWithTag("GameManager");
        
    }

    private void Update()
    {
        if (bossAI.dead)
        {
            if (!a)
            {
                a = true;
                Invoke("ClearBoss", 4);
            }
        }
    }

    public void ClearBoss()
    {
        gM.GetComponent<DataManager>().Save_PlayerData_To_InGameData();
        gM.GetComponent<AudioSource>().Stop();
        finishUI.GetComponent<CanvasGroup>().alpha = 1;
        gM.GetComponent<ScenesLoader>().Invoke("LoadScene",3);   
    }
}
