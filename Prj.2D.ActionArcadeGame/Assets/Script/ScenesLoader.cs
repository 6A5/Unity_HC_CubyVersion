using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    public string loadSceneName; // 要讀取的場景名稱

    public Text chosenStage; // 選中關卡文字
    int stage = 1;

    /// <summary>
    /// 場景讀取
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene(loadSceneName);
    }

    public void ReloadScene()
    {
        //print(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StageChose()
    {
        stage++;
        if (stage > 6)
        {
            stage = 1;
        }

        switch (stage)
        {
            case 1:
                loadSceneName = "Highland1";
                break;
            case 2:
                loadSceneName = "Highland2";
                break;
            case 3:
                loadSceneName = "Cave1";
                break;
            case 4:
                loadSceneName = "Cave2";
                break;
            case 5:
                loadSceneName = "Lab1";
                break;
            case 6:
                loadSceneName = "Boss";
                break;
        }

        chosenStage.text = loadSceneName;
    }
}
