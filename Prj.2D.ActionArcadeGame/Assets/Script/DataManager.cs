using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// 儲存資料到資料庫
    /// </summary>
    public void Save_PlayerData_To_InGameData()
    {
        PlayerControl dataSource = player.GetComponent<PlayerControl>();

        InGameData.thisTurn_Life = dataSource.life;
        InGameData.thisTurn_Score = dataSource.score;

    }

    /// <summary>
    /// 讀取資料到玩家身上
    /// </summary>
    public void Load_PlayerData_From_InGameData()
    {
        PlayerControl dataTarget = player.GetComponent<PlayerControl>();

        dataTarget.life = InGameData.thisTurn_Life;
        dataTarget.score = InGameData.thisTurn_Score;
    }
}
